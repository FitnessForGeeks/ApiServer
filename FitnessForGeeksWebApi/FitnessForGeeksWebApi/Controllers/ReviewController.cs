using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessForGeeksWebApi.Controllers.RequestDataClasses;
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
        public IActionResult GetAllByRecipeIdWithPage([FromQuery] int id, [FromQuery] int pageNumber)
        {
            return Ok(reviewManager.GetAllByRecipeId(id, 5 * (pageNumber - 1) , 5));
        }

		[HttpPost]
		public IActionResult PostReview([FromBody] PostReviewPostData data)
		{ 
			int? id = reviewManager.Create(data).Value;
			if (id.HasValue)
				return Ok(id);
			else
				return StatusCode(500);
		}
    }
}