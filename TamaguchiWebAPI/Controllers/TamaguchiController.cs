﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TamaguchiBL.Models;
using TamaguchiWebAPI.DTO;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


namespace TamaguchiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TamaguchiController : ControllerBase
    {

        TamaguchiContext context;

        public TamaguchiController(TamaguchiContext context)
        {
            this.context = context;
        }

        [Route("Login")]
        [HttpPost]

        public PlayerDTO Login([FromBody] UserDTO user)
        {
            Player p = context.Login(user.Email, user.Pass);

            if (p != null)
            {
                PlayerDTO pDTO = new PlayerDTO(p);
                HttpContext.Session.SetObject("loggedin", pDTO);
                Response.StatusCode = (int)HttpStatusCode.OK;
                return pDTO;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }

        }


        [Route("Logout")]
        [HttpGet]

        public void Logout()
        {
            if (HttpContext.Session.GetObject<PlayerDTO>("loggedin") != null)
            {
                HttpContext.Session.Clear();
                Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }


        [Route("Lucas")]
        [HttpGet]

        public string Lucas()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            Response.WriteAsync("Lucas The King");
            return "lucas The King";
        }


        [Route("IsLoggedin")]
        [HttpGet]

        public bool IsLoggedin()
        {
            return HttpContext.Session.GetObject<bool>("loggedin");
        }



        [Route("IsEmailExists")]
        [HttpPost]

        public bool IsEmailExists([FromBody] string email)
        {

            Player p = this.context.Players.FirstOrDefault(p => p.Email == email);
            if (p != null)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return true;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return false;
            }

        }

        [Route("IsUserNameExists")]
        [HttpPost]

        public bool IsUserNameExists([FromBody] string userName)
        {
            Player p = this.context.Players.FirstOrDefault(p => p.UserName == userName);
            if (p != null)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return true;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return false;
            }
        }


        [Route("AddPlayer")]
        [HttpPost]

        public PlayerDTO AddPlayer([FromBody] PlayerDTO p)
        {
            Player p1 = this.context.Players.CreateProxy(new Player
            {
                BirthDate = p.BirthDate,
                Email = p.Email,
                FirstName = p.FirstName,
                Gender = p.Gender,
                LastName = p.LastName,
                Pass = p.Pass,
                UserName = p.UserName
            });

            try
            {
                this.context.Players.Add(p1);
                this.context.SaveChanges();
                return p;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [Route("AddActivity")]
        [HttpPost]
        public ActivityDTO AddActivity([FromBody] ActivityDTO a)
        {
            Activity a1 = this.context.Activities.CreateProxy(new Activity
            {
                ActivityName = a.ActivityName,
                CategoryId = a.CategoryId,
                ImprovementHappiness = a.ImprovementHappiness,
                ImprovementHunger = a.ImprovementHunger,
                ImprovementHygiene = a.ImprovementHygiene
            });


            try
            {
                this.context.Activities.Add(a1);
                this.context.SaveChanges();
                return a;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [Route("GetActivityHistory")]
        [HttpPost]

        public List<ActivityHistoryDTO> GetActivitiyHistory([FromBody] UserDTO user)
        {
            Player p = this.context.Players.FirstOrDefault(p1 => p1.Email == user.Email);
            if (p.CurrentPet != null)
            {
                List<ActivitiesHistory> lah = this.context.ActivitiesHistories.Where(p2 => p2.PetCode == p.CurrentPetId).ToList();

                List<ActivityHistoryDTO> lahd = new List<ActivityHistoryDTO>();

                foreach (ActivitiesHistory ah in lah)
                {
                    lahd.Add(new ActivityHistoryDTO { ActivityDate = ah.ActivityDate, ActivityName = ah.ActivityCodeNavigation.ActivityName, PetWeight = ah.PetWeight, Age = ah.Age });
                }

                Response.StatusCode = (int)HttpStatusCode.OK;
                return lahd;

            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }


        }
    }
}
