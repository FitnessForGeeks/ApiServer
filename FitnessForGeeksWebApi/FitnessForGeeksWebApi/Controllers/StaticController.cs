using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace FitnessForGeeksWebApi.Controllers
{
    [Route("api/[controller]")]
    public class StaticController : Controller
    {
        [HttpGet]
        [Route("{username}/profilePicture")]
        public IActionResult GetProfilePicture([FromRoute] string username)
        {
            var path = $"./static/{username}/profilePicture.jpg";
            byte[] bytes;
            if (System.IO.File.Exists(path))
            {
                bytes = System.IO.File.ReadAllBytes(path);
            }
            else
            {
                bytes = System.IO.File.ReadAllBytes("./static/profilePicture.jpg");
            }
            return File(bytes, "image/jpeg");
        }

        [HttpPost]
        [Route("{username}/profilePicture")]
		public IActionResult PostProfilePicture([FromRoute] string username)
		{
            var file = Request.Form.Files[0];
            var path = $"./static/{username}/profilePicture.jpg";
			if(file != null)
			{
                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Create(path);
                }
				using(var stream = new FileStream(path, FileMode.Truncate))
				{
					file.CopyTo(stream);
				}
			}
			return Ok();
		}
        
        [HttpGet]
        [Route("recipes/{title}")]
        public IActionResult GetRecipePicture([FromRoute] string title)
        {
            var path = $"./static/recipes/{title}.jpg";
            byte[] bytes;
            if (System.IO.File.Exists(path))
            {
                bytes = System.IO.File.ReadAllBytes(path);
            }
            else
            {
                bytes = System.IO.File.ReadAllBytes("./static/defaultRecipe.jpg");
            }
            return File(bytes, "image/jpeg");
        }


        [HttpPost]
        [Route("recipes/{title}")]
        public IActionResult PostRecipePicture([FromRoute] string title)
        {
            if(Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                var path = $"./static/recipes/{title}.jpg";
                if(file != null)
                {
                    if (!System.IO.File.Exists(path))
                    {
                        System.IO.File.Create(path).Close();
                    }
                    using(var stream = new FileStream(path, FileMode.Truncate))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok();
            }
            return BadRequest();
        }

		[HttpGet]
		[Route("eat-it-button")]
		public IActionResult GetEatItButtonIcon()
		{
			return File(System.IO.File.ReadAllBytes("./static/eat-it-button.svg"), "image/svg+xml");
		}
    }

}