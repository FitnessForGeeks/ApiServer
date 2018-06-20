using FitnessForGeeksWebApi.Database;
using FitnessForGeeksWebApi.RecipeDB;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessForGeeksWebApi.Controllers
{
    [Route("api/[controller]")]
    public class RecipeController : Controller
    {
        RecipeManager manager = new RecipeManager();

        [HttpGet]
        public IActionResult GetAll([FromQuery] int? pageNumber, [FromQuery] string query, [FromQuery] string sortText, [FromQuery] bool? isAscending)
        {
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

    }
}
