using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Trustify.Backend.FeaturesService.Controllers
{
    [Route("image-content")]
    [ApiController]
    public class ImageContentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetImage()
        {
            return Ok();
        }
        // Adding new image
        // Getting one image/download
        // Getting all images related to something
        // Resizing image or something like that
        // Changing order of images
    }
}
