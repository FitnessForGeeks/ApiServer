using System;
using Microsoft.AspNetCore.Mvc;
using FitnessForGeeksWebApi.Database;
using System.Collections.Generic;
using FitnessForGeeksWebApi.Controllers.RequestDataClasses;
using FitnessForGeeksWebApi.Database.AccountDB;

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
        public List<Account> Get()
        {
            return manager.GetAll();
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
            if (acc == null)
                return NotFound();
            if (acc.Password == data.Password)
            {
                Response.Cookies.Append("authKey", acc.AuthKey, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddDays(7)
                });
                return Ok(acc);
            }
            return Forbid();
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Post()
        {
            Response.Cookies.Delete("authKey");
            return Ok();
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate()
        {
            Request.Cookies.TryGetValue("authKey", out string authKey);
            Account acc = manager.getByAuthKey(authKey);
            if (acc == null)
                return StatusCode(403);

            return Ok(acc);
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
