using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace TamaguchiBL.Models
{
    public partial class TamaguchiContext
    {
        //Log in method. Return a player or null if not succeed!
        public Player Login(string email, string pswd)
        {

             try
            {
                return this.Players.FirstOrDefault((p => (p.Email == email || p.UserName == email) && p.Pass == pswd));
                //Player p = this.Players.Single((p => (p.Email == email || p.UserName == email) && p.Pass == pswd));
                //return p;
            }
            catch (Exception e)
            {
                
                throw new Exception("Could not Login. error retreiving Data", e);
            }
        }


        public void AddPlayer(Player p)//No exception is expected bc it has already been checked 
        {
            this.Players.Add(p);
            this.SaveChanges();
            
        }
        public Activity GetFood(int foodCode)
        {
            return this.Activities.Single(f => f.CategoryId == 1 && f.ActivityCode == foodCode);
        }

        public Activity GetClean(int cleanCode)
        {
            return this.Activities.Single(c => c.CategoryId == 3 && c.ActivityCode == cleanCode);
        }

        public Activity GetGame(int gameCode)
        {
            return this.Activities.Single(g => g.CategoryId == 2 && g.ActivityCode == gameCode);
        }


        public List<object> GetFoodList()
        {
            List<object> foodList = (from food in this.Activities
                                     where food.CategoryId == 1
                                     select new
                                     {
                                         Number = food.ActivityCode,
                                         Food = food.ActivityName,
                                         Improvment = food.ImprovementHunger
                                     }).ToList<object>();
            return foodList;
        }
        
        public List<object> GetCleanList()
        {
            List<object> cleanList = (from clean in this.Activities
                                      where clean.CategoryId == 3
                                      select new
                                      {
                                          Number = clean.ActivityCode,
                                          Clean = clean.ActivityName,
                                          Improvment = clean.ImprovementHygiene
                                      }).ToList<object>();
            return cleanList;
        }

        public List<object> GetGameList()
        {
            List<object> cleanList = (from game in this.Activities
                                      where game.CategoryId == 2
                                      select new
                                      {
                                          Number = game.ActivityCode,
                                          Game = game.ActivityName,
                                          Improvment = game.ImprovementHappiness
                                      }).ToList<object>();
            return cleanList;
        }


        public void AddActivityHistory(ActivitiesHistory aH)
        {
            this.ActivitiesHistories.Add(aH);
        }

        public void AddPet(Pet p)
        {
            this.Pets.Add(p);
        }

        public void AddActivity(Activity a)
        {
            
            this.Activities.Add(a);
            this.SaveChanges();
        }



    }
}
