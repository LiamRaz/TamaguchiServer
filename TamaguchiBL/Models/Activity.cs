using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamaguchiBL.Models
{
    [Index(nameof(ActivityName), Name = "idx_ActivityName")]
    public partial class Activity
    {
        public Activity()
        {
            ActivitiesHistories = new HashSet<ActivitiesHistory>();
        }

        [Required]
        [StringLength(255)]
        public string ActivityName { get; set; }
        [Key]
        public int ActivityCode { get; set; }
        public int ImprovementHappiness { get; set; }
        public int ImprovementHunger { get; set; }
        public int ImprovementHygiene { get; set; }
        [Column("CategoryID")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Activities")]
        public virtual Category Category { get; set; }
        [InverseProperty(nameof(ActivitiesHistory.ActivityCodeNavigation))]
        public virtual ICollection<ActivitiesHistory> ActivitiesHistories { get; set; }
    }
}
