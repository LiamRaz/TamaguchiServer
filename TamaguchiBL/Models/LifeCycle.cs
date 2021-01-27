using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamaguchiBL.Models
{
    [Table("LifeCycle")]
    [Index(nameof(CycleName), Name = "idx_Cycle_Name")]
    public partial class LifeCycle
    {
        public LifeCycle()
        {
            Pets = new HashSet<Pet>();
        }

        [Key]
        public int LifeCycleCode { get; set; }
        public int CycleTime { get; set; }
        [Required]
        [Column("Cycle_Name")]
        [StringLength(255)]
        public string CycleName { get; set; }

        [InverseProperty(nameof(Pet.LifeCycleCodeNavigation))]
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
