using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessForGeeksWebApi.Database.ReviewDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessForGeeksWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        ReviewManager reviewManager = new ReviewManager();

        [HttpGet]
        public IActionResult GetAllByRecipeid([FromQuery] int id)
        {
            return Ok(reviewManager.GetAllByRecipeId(id));
        }
    }
}