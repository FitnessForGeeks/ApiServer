using FitnessForGeeksWebApi.Database;
using FitnessForGeeksWebApi.RecipeDB;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessForGeeksWebApi.Controllers
{
    [Route("api/[controller]")]
    public class RecipeController : Controller
    {
        RecipeManager manager = new RecipeManager();

        [HttpGet]
        public IActionResult GetAll([FromQuery]int? accountId, [FromQuery] int? pageNumber, [FromQuery] string sortText, [FromQuery] bool? isAscending, [FromQuery] string query = "")
        {
            if (query == null)
                query = "";
			if(pageNumber.HasValue && isAscending.HasValue && query != null && sortText != null)
			{
				return Ok(manager.GetAll(pageNumber.Value, isAscending.Value, query, sortText));
			}
			else
				return Ok(manager.GetAll());
        }

        [HttpGet]
        [Route("search")]
        public IActionResult GetByQuery([FromQuery]string query)
        {
            return Ok(manager.GetByQuery(query));
        }


        [HttpPost]
        public IActionResult Post([FromBody]Recipe recipe)
        {
            try
            {
                manager.Create(recipe);
            }
            catch (MySqlException ex)
            {
                return StatusCode(409);
            }
            return Ok();
        }

    }
}
