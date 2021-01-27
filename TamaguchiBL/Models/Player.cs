using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TamaguchiBL.Models
{
    [Index(nameof(Email), Name = "idx_Email")]
    [Index(nameof(UserName), Name = "idx_UserName")]
    public partial class Player
    {
        public Player()
        {
            ActivitiesHistories = new HashSet<ActivitiesHistory>();
            Pets = new HashSet<Pet>();
        }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        [Key]
        public int PlayerCode { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Gender { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime BirthDate { get; set; }
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }
        [Required]
        [StringLength(255)]
        public string Pass { get; set; }
        [Column("CurrentPetID")]
        public int? CurrentPetId { get; set; }

        [ForeignKey(nameof(CurrentPetId))]
        [InverseProperty(nameof(Pet.Player))]
        public virtual Pet CurrentPet { get; set; }
        [InverseProperty(nameof(ActivitiesHistory.PlayerCodeNavigation))]
        public virtual ICollection<ActivitiesHistory> ActivitiesHistories { get; set; }
        [InverseProperty(nameof(Pet.PlayerCodeNavigation))]
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
