using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamaguchiBL.Models
{
    [Index(nameof(CategoryName), Name = "idx_CategoryName")]
    public partial class Category
    {
        public Category()
        {
            Activities = new HashSet<Activity>();
        }

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; }
        [Key]
        [Column("CategoryID")]
        public int CategoryId { get; set; }

        [InverseProperty(nameof(Activity.Category))]
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
