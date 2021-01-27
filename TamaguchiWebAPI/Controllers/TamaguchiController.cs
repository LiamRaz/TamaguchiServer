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
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                Response.WriteAsync("Login Successfully");
                PlayerDTO pDTO = new PlayerDTO(p);
                HttpContext.Session.SetObject("loggedin", pDTO);
                return pDTO;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                Response.WriteAsync("Login failed");
                return null;
            }

        }


        [Route("Logout")]
        [HttpGet]

        public void Logout()
        {
            if(HttpContext.Session.GetObject<PlayerDTO>("loggedin") != null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                Response.WriteAsync("bye bye");
                HttpContext.Session.Clear();
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            }
        }

        

    }
}
