using System;
using System.Collections.Generic;
using System.Text;


namespace TamaguchiBL.Models
{
    public partial class Activity
    {
        public void InsertActivity(string name, int iHap, int iHun, int iHyg, int catId)
        {
            this.ActivityName = name;
            this.ImprovementHappiness = iHap;
            this.ImprovementHunger = iHun;
            this.ImprovementHygiene = iHyg;
            this.CategoryId = catId;
            
        }
    }
}
