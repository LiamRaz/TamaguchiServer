using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamaguchiBL.Models
{
    [Index(nameof(PetName), Name = "idx_PetName")]
    public partial class Pet
    {
        public Pet()
        {
            ActivitiesHistories = new HashSet<ActivitiesHistory>();
        }

        [Required]
        [StringLength(255)]
        public string PetName { get; set; }
        [Key]
        public int PetCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        [Column("Pet_Weight")]
        public int PetWeight { get; set; }
        public int HungerStatus { get; set; }
        public int HappinessStatus { get; set; }
        public int HygieneStatus { get; set; }
        public int LifeCycleCode { get; set; }
        public int HealthCode { get; set; }
        public int PlayerCode { get; set; }

        [ForeignKey(nameof(HealthCode))]
        [InverseProperty(nameof(HealthStatus.Pets))]
        public virtual HealthStatus HealthCodeNavigation { get; set; }
        [ForeignKey(nameof(LifeCycleCode))]
        [InverseProperty(nameof(LifeCycle.Pets))]
        public virtual LifeCycle LifeCycleCodeNavigation { get; set; }
        [ForeignKey(nameof(PlayerCode))]
        [InverseProperty("Pets")]
        public virtual Player PlayerCodeNavigation { get; set; }
        [InverseProperty("CurrentPet")]
        public virtual Player Player { get; set; }
        [InverseProperty(nameof(ActivitiesHistory.PetCodeNavigation))]
        public virtual ICollection<ActivitiesHistory> ActivitiesHistories { get; set; }
    }
}
