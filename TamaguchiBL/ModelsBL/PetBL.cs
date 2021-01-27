using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TamaguchiBL.Models
{
    public partial class Pet
    {

        public void Improve(Activity a)
        {

            this.HungerStatus += a.ImprovementHunger;
            if (this.HungerStatus < 0)
                this.HungerStatus = 0;

            this.HygieneStatus += a.ImprovementHygiene;
            if (this.HygieneStatus > 100)
                this.HygieneStatus = 100;
            if (this.HygieneStatus < 0)
                this.HygieneStatus = 0;

            this.HappinessStatus += a.ImprovementHappiness;
            if (this.HappinessStatus > 100)
                this.HappinessStatus = 100;
            if (this.HappinessStatus < 0)
                this.HappinessStatus = 0;

        }

        public List<object> GetActivityHistory(int option)
        {
            return this.ActivitiesHistories.OrderByDescending(a => a.ActivityDate).Take(option).Select(a => new
            {
                name = this.PetName,
                date = a.ActivityDate,
                age = this.Age,
                lifeCycle = this.LifeCycleCodeNavigation.CycleName,
                activity = a.ActivityCodeNavigation.ActivityName
            }).ToList<object>();
        }

        public void InsertPet(string name, int weight, int improvment, int age, int lifeCycleID, int healthID, int playerId)
        {
            this.PetName = name;
            this.PlayerCode = playerId;
            this.BirthDate = DateTime.Now;
            this.PetWeight = weight;
            this.HappinessStatus = improvment;
            this.HungerStatus = improvment;
            this.HygieneStatus = improvment;
            this.Age = age;
            this.LifeCycleCode = lifeCycleID;
            this.HealthCode = healthID;
        }



    }
}
