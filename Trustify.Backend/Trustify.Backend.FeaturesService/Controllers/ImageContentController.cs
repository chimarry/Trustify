using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trustify.Backend.FeaturesCore.AutoMapper;
using Trustify.Backend.FeaturesCore.DTO;
using Trustify.Backend.FeaturesCore.Services;
using Trustify.Backend.FeaturesService.Mapper;
using Trustify.Backend.FeaturesService.Models;

namespace Trustify.Backend.FeaturesService.Controllers
{
    [Route("image-content")]
    [ApiController]
    public class ImageContentController : ControllerBase
    {
        private readonly IImageContentService imageContentService;
        private readonly IMapper mapper;

        public ImageContentController(IImageContentService imageContentService, IMapper mapper)
        {
            this.imageContentService = imageContentService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddImage([FromForm] ImageContentWrapper image)
        {
            BasicFileInfo? file = image.Image?.AsBasicFileInfo();
            ImageContentDTO dto = mapper.Map<ImageContentDTO>(image);

            return HttpResultMessage.FilteredResult(await imageContentService.AddImage(dto, file));
        }

        [HttpGet("{imageContentId}")]
        public async Task<IActionResult> GetImage([FromRoute] int imageContentId)
        {
            return HttpResultMessage.FilteredResult(await imageContentService.GetImage(imageContentId));
        }

        [HttpDelete("{imageContentId}")]
        public async Task<IActionResult> DeleteImage([FromRoute] int imageContentId)
        {
            return HttpResultMessage.FilteredResult(await imageContentService.Delete(imageContentId));
        }

        [SwaggerResponse(StatusCodes.Status200OK, "The result is returned.", typeof(ImageContentDTO))]
        [HttpGet]
        public IActionResult GetAll([FromQuery] int skip = 0, [FromQuery] int take = 0)
        {
            return HttpResultMessage.FilteredResult(imageContentService.GetAll(skip, take));
        }

        [HttpGet("{imageContentId}/download")]
        public async Task<IActionResult> DownloadImage([FromRoute] int imageContentId)
        {
            return HttpResultMessage.FilteredResult(await imageContentService.DownloadImage(imageContentId), ContentType.File);
        }
    }
}
