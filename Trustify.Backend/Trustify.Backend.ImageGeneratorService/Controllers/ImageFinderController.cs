using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Trustify.Backend.ImageGeneratorService.Controllers
{
    [Route("image-finder")]
    [ApiController]
    public class ImageFinderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Find()
        {
            return Ok();
        }
        // find image based on the word
        // upload image to images
        // download image
    }
}
