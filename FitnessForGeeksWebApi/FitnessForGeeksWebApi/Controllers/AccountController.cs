using System;
using Microsoft.AspNetCore.Mvc;
using FitnessForGeeksWebApi.Database;
using System.Collections.Generic;
using FitnessForGeeksWebApi.Controllers.RequestDataClasses;
using FitnessForGeeksWebApi.Database.AccountDB;
using FitnessForGeeksWebApi.Database.EatenRecipeDB;
using MySql.Data.MySqlClient;

namespace FitnessForGeeksWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly AccountManager manager;
        private readonly EatenRecipeManager eatenRecipeManager;

        public AccountController()
        {
            this.manager = new AccountManager();
            this.eatenRecipeManager = new EatenRecipeManager();
        }

        [HttpGet]
        public List<Account> Get()
        {
            return manager.GetAll();
        }

		[HttpPut]
		public IActionResult UpdateAccount([FromBody] Account account)
		{
			try
			{
				manager.UpdateAccount(account);
			}
			catch (MySqlException ex)
			{
				return StatusCode(409);
			}
			return Ok();
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
		public IActionResult LogOut()
		{
			Response.Cookies.Delete("authKey");
			return Ok();
		}

		[HttpGet]
		[Route("eatenRecipes/today")]
		public IActionResult GetEatenRecipesToday([FromQuery] int accountId)
		{
			return Ok(manager.GetById(accountId).RecipesEatenToday);
		}

		[HttpGet]
		[Route("myRecipes")]
		public IActionResult GetMyRecipes([FromQuery] int accountId)
		{
			return Ok(manager.GetById(accountId).MyRecipes);
		}
		
		[HttpPost]
        [Route("eatRecipe")]
        public IActionResult EatRecipe([FromQuery] int accountId, [FromQuery] int recipeId)
        {
			eatenRecipeManager.Create(accountId, recipeId);
            return Ok();
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate()
        {
            Request.Cookies.TryGetValue("authKey", out string authKey);
            Account acc = manager.GetByAuthKey(authKey);
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
    }
}
