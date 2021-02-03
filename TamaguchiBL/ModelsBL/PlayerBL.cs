using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace TamaguchiBL.Models
{
    public partial class Player
    {
        public Player(string fName, string lName, DateTime birthDate, string gen, string email, string uName, string pass)
        {
            BirthDate = birthDate;
            Email = email;
            FirstName = fName;
            Gender = gen;
            LastName = lName;
            Pass = pass;
            UserName = uName;
        }

        public List<object> GetPetStats()
        {
            try
            {
                return this.Pets.Select(p => new
                {
                    Name = p.PetName,
                    age = p.Age,
                    birthDate = p.BirthDate,
                    Happines = p.HappinessStatus,
                    Hunger = p.HungerStatus,
                    Hygiene = p.HygieneStatus,
                    Weight = p.PetWeight,
                    lifeCycle = p.LifeCycleCodeNavigation.CycleName,
                    Health = p.HealthCodeNavigation.HealthName
                }).ToList<object>();

            }
            catch (Exception e)
            {
                Console.WriteLine("Restart your application!");
                return null;
            }


        }

        public void InsertPlayer(string firstName, string lastName, string email, string gender, DateTime BirthDay, string userName, string pass)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Gender = gender;
            this.BirthDate = BirthDay;
            this.UserName = userName;
            this.Pass = pass;
            this.CurrentPetId = null;

        }
    }
}
