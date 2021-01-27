using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TamaguchiBL.Models;

namespace TamaguchiWebAPI.DTO
{
    public class PlayerDTO
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PlayerCode { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public PlayerDTO()
        {

        }

        public PlayerDTO(Player p)
        {
            this.FirstName = p.FirstName;
            this.LastName = p.LastName;
            this.PlayerCode = p.PlayerCode;
            this.Email = p.Email;
            this.Gender = p.Gender;
            this.BirthDate = p.BirthDate;
        }
    }
}
