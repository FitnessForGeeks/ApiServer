using System;
using Microsoft.AspNetCore.Mvc;
using FitnessForGeeksWebApi.Database;
using System.Collections.Generic;
using FitnessForGeeksWebApi.Controllers.RequestDataClasses;

namespace FitnessForGeeksWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly AccountManager manager;

        public AccountController()
        {
            this.manager = new AccountManager();
        }

        [HttpGet]
        public List<Account> Get([FromQuery]int id)
        {
            return manager.Get();
        }
        
        [HttpPost]
        [Route("login")]
        public IActionResult Post([FromBody] LoginPostData data)
        {
            if (data.Username == null || data.Password == null)
            {
                return StatusCode(400);
            }
            Account acc = manager.GetByUsername(data.Username);
            if (acc != null && acc.Password == data.Password)
            {
                Response.Cookies.Append("authKey", acc.AuthKey, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    HttpOnly = true
                });
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Post()
        {
            Response.Cookies.Delete("authKey");
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateAccountPostData data)
        {
            if (manager.Create(data))
            {
                return Ok();
            }
            return StatusCode(409);
        }

        [HttpDelete]
        public void Delete([FromBody]int id)
        {
        }
    }
}
