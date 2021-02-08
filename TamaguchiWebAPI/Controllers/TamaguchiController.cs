using Microsoft.AspNetCore.Http;
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


        [Route("IsEmailExists")]
        [HttpPost]

        public bool IsEmailExists([FromBody] string email)
        {
            try
            {
                Player p = this.context.Players.FirstOrDefault(p => p.Email == email);
                if (p != null)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return true;
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return false;
                }
            }
            catch(Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return false;
            }

        }

        [Route("IsUserNameExists")]
        [HttpPost]

        public bool IsUserNameExists([FromBody] string userName)
        {
            try
            {
                Player p = this.context.Players.FirstOrDefault(p => p.UserName == userName);
                if (p != null)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return true;
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return false;
                }
            }
            catch(Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return false;
            }
        }


        [Route("AddPlayer")]
        [HttpPost]

        public PlayerDTO AddPlayer([FromBody] PlayerDTO p)
        {
            try
            {
                Player p1 = new Player(p.FirstName, p.LastName,
                p.BirthDate, p.Gender, p.Email, p.UserName, p.Pass);

                this.context.AddPlayer(p1);
                return p;
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("AddActivity")]
        [HttpPost]
        public ActivityDTO AddActivity([FromBody] ActivityDTO a)
        {
            try
            {
                Activity a1 = new Activity(a.ActivityName, a.CategoryId,
                a.ImprovementHappiness, a.ImprovementHunger, a.ImprovementHygiene);

                this.context.AddActivity(a1);
                return a;
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("GetActivityHistory")]
        [HttpPost]

        public List<ActivityHistoryDTO> GetActivitiyHistory()
        {
            try
            {

                PlayerDTO p1 = HttpContext.Session.GetObject<PlayerDTO>("loggedin");
                if (p1 != null)
                {
                    Player p = this.context.Players.FirstOrDefault<Player>(p => p1.Email == p.Email);
                    if (p.CurrentPet != null)
                    {
                        List<ActivityHistoryDTO> lst = p.CurrentPet.ActivitiesHistories.OrderByDescending(a => a.ActivityDate).Select(aH => new ActivityHistoryDTO
                        {

                            Name = aH.ActivityCodeNavigation.ActivityName,
                            Age = aH.Age,
                            Date = aH.ActivityDate,
                            CategoryName = aH.ActivityCodeNavigation.Category.CategoryName,
                            LifeCycle = aH.PetCodeNavigation.LifeCycleCodeNavigation.CycleName

                        }).ToList<ActivityHistoryDTO>();

                        Response.StatusCode = (int)HttpStatusCode.OK;
                        return lst;
                    }
                    else
                    {
                        Response.StatusCode = (int)HttpStatusCode.OK;
                        return null;
                    }

                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return null;
                }
            }
            catch(Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }



        [Route("GetPets")]
        [HttpGet]

        public List<PetStatsDTO> GetPets()
        {
            try
            {
                PlayerDTO p1 = HttpContext.Session.GetObject<PlayerDTO>("loggedin");

                if (p1 != null)
                {
                    Player p = this.context.Players.FirstOrDefault<Player>(p => p1.Email == p.Email);
                    List<PetStatsDTO> pets = p.Pets.Select(pet => new PetStatsDTO
                    {
                        Name = pet.PetName,
                        Age = pet.Age,
                        BirthDate = pet.BirthDate,
                        Happiness = pet.HappinessStatus,
                        Hunger = pet.HungerStatus,
                        Hygiene = pet.HygieneStatus,
                        Weight = pet.PetWeight,
                        LifeCycle = pet.LifeCycleCodeNavigation.CycleName,
                        Health = pet.HealthCodeNavigation.HealthName

                    }).ToList<PetStatsDTO>();

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return pets;
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return null;
                }
            }
            catch(Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }


    }
}
