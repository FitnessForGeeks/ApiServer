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
        public IActionResult getAll()
        {
            return Ok(manager.GetAll());
        }

        [HttpGet]
        [Route("search")]
        public IActionResult getByQuery([FromQuery]string query)
        {
            return Ok(manager.GetByQuery(query));
        }

    }
}
