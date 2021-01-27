using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace TamaguchiBL.Models
{
    public partial class ActivitiesHistory
    {

        public void InsertActivity(Pet p, Activity a)
        {
            this.ActivityCode = a.ActivityCode;
            this.PetCode = p.PetCode;
            this.PlayerCode = p.PlayerCode;
            this.PetWeight = p.PetWeight;
            this.PetHealthStatus = p.HealthCode;
            this.Age = p.Age;
            this.LifeCycleCode = p.LifeCycleCode;
            this.ActivityDate = DateTime.Now;

        }

    }
}
