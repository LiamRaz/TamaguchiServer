using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TamaguchiBL.Models;

namespace TamaguchiWebAPI.DTO
{
    public class PetDTO
    {

        public string PetName { get; set; }
        public int PetCode { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public int PetWeight { get; set; }
        public int HungerStatus { get; set; }
        public int HappinessStatus { get; set; }
        public int HygieneStatus { get; set; }
        public int PlayerCode { get; set; }

        public PetDTO()
        {

        }

        public PetDTO(Pet p)
        {
            this.PetName = p.PetName;
            this.PetCode = p.PetCode;
            this.BirthDate = p.BirthDate;
            this.Age = p.Age;
            this.PetWeight = p.PetWeight;
            this.HungerStatus = p.HungerStatus;
            this.HappinessStatus = p.HappinessStatus;
            this.HygieneStatus = p.HygieneStatus;
            this.PlayerCode = p.PlayerCode;
        }

    }
}
