using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TamaguchiBL.Models;
using TamaguchiWebAPI.DTO;

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
            
            if(p != null)
            {
                PlayerDTO pDTO = new PlayerDTO(p);
                HttpContext.Session.SetObject("loggedin", pDTO);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return pDTO;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }

        }


        [Route("Logout")]
        [HttpGet]

        public void Logout()
        {
            if(HttpContext.Session.GetObject<PlayerDTO>("loggedin") != null)
            {
                HttpContext.Session.Clear();
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            }
        }


        [Route("Lucas")]
        [HttpGet]

        public string Lucas()
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            Response.WriteAsync("Lucas The King");
            return "lucas The King";
        }


        [Route("IsLoggedin")]
        [HttpGet]

        public bool IsLoggedin()
        {
            return HttpContext.Session.GetObject<bool>("loggedin");
        }


    }
}
