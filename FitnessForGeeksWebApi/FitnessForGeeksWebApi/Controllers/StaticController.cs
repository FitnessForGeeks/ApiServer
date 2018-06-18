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
    }
}