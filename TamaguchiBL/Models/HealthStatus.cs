using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamaguchiBL.Models
{
    [Table("HealthStatus")]
    [Index(nameof(HealthName), Name = "idx_HealthName")]
    public partial class HealthStatus
    {
        public HealthStatus()
        {
            ActivitiesHistories = new HashSet<ActivitiesHistory>();
            Pets = new HashSet<Pet>();
        }

        [Key]
        public int HealthCode { get; set; }
        [Required]
        [StringLength(255)]
        public string HealthName { get; set; }

        [InverseProperty(nameof(ActivitiesHistory.PetHealthStatusNavigation))]
        public virtual ICollection<ActivitiesHistory> ActivitiesHistories { get; set; }
        [InverseProperty(nameof(Pet.HealthCodeNavigation))]
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
