using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trustify.Backend.FeaturesCore.AutoMapper;
using Trustify.Backend.FeaturesCore.DTO;
using Trustify.Backend.FeaturesCore.Services;
using Trustify.Backend.FeaturesService.Mapper;
using Trustify.Backend.FeaturesService.Models;

namespace Trustify.Backend.FeaturesService.Controllers
{
    [Route("textual-content")]
    [ApiController]
    public class TextualContentController(ITextualContentService contentService, IMapper mapper) : ControllerBase
    {
        private readonly ITextualContentService contentService = contentService;
        private readonly IMapper mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TextualContentWrapper wrapper)
        {
            var content = mapper.Map<TextualContentDTO>(wrapper);
            content.Lenght = content.Text.Length;
            ResultMessage<TextualContentDTO> dto = await contentService.AddTextualContent(content);
            return HttpResultMessage.FilteredResult(dto);
        }

        [HttpDelete("{textualContentId}")]
        public async Task<IActionResult> Delete([FromRoute] long textualContentId)
        {
            ResultMessage<bool> isDeleted = await contentService.DeleteTextualContent(textualContentId);
            return HttpResultMessage.FilteredResult(isDeleted);
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int skip = 0, [FromQuery] int take = 0)
        {
            return HttpResultMessage.FilteredResult(contentService.GetAllTextualContent(skip, take));
        }

        [SwaggerResponse(StatusCodes.Status200OK, "The result is returned.", typeof(TextualContentDTO))]
        [HttpGet("{textualContentId}")]
        public async Task<IActionResult> Get([FromRoute] int textualContentId)
        {
            ResultMessage<TextualContentDTO> dto = await contentService.GetTextualContent(textualContentId);
            return HttpResultMessage.FilteredResult(dto);
        }
    }
}
