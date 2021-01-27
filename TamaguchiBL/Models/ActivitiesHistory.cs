using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamaguchiBL.Models
{
    [Table("ActivitiesHistory")]
    [Index(nameof(ActivityDate), Name = "idx_ActivityDate")]
    [Index(nameof(Age), Name = "idx_Age")]
    [Index(nameof(LifeCycleCode), Name = "idx_LifeCycleCode")]
    public partial class ActivitiesHistory
    {
        public int LifeCycleCode { get; set; }
        public int Age { get; set; }
        public int PlayerCode { get; set; }
        [Key]
        public int PetCode { get; set; }
        public int ActivityCode { get; set; }
        [Key]
        [Column(TypeName = "datetime")]
        public DateTime ActivityDate { get; set; }
        public int PetWeight { get; set; }
        public int PetHealthStatus { get; set; }

        [ForeignKey(nameof(ActivityCode))]
        [InverseProperty(nameof(Activity.ActivitiesHistories))]
        public virtual Activity ActivityCodeNavigation { get; set; }
        [ForeignKey(nameof(PetCode))]
        [InverseProperty(nameof(Pet.ActivitiesHistories))]
        public virtual Pet PetCodeNavigation { get; set; }
        [ForeignKey(nameof(PetHealthStatus))]
        [InverseProperty(nameof(HealthStatus.ActivitiesHistories))]
        public virtual HealthStatus PetHealthStatusNavigation { get; set; }
        [ForeignKey(nameof(PlayerCode))]
        [InverseProperty(nameof(Player.ActivitiesHistories))]
        public virtual Player PlayerCodeNavigation { get; set; }
    }
}
