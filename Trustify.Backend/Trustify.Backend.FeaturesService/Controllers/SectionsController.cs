using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trustify.Backend.FeaturesCore.DTO;
using Trustify.Backend.FeaturesCore.Services;
using Trustify.Backend.FeaturesService.Mapper;
using Trustify.Backend.FeaturesService.Models;

namespace Trustify.Backend.FeaturesService.Controllers
{
    [Route("sections")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ISectionsService sectionService;

        public SectionsController(IMapper mapper, ISectionsService sectionService)
        {
            this.mapper = mapper;
            this.sectionService = sectionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSection([FromBody] SectionWrapper section)
        {
            SectionDTO sectiondto = mapper.Map<SectionDTO>(section);
            return HttpResultMessage.FilteredResult(await sectionService.AddSection(sectiondto));
        }

        [HttpDelete("{sectionId}")]
        public async Task<IActionResult> DeleteSection([FromRoute] long sectionId)
        {
            return HttpResultMessage.FilteredResult(await sectionService.DeleteSection(sectionId));
        }

        [SwaggerResponse(StatusCodes.Status200OK, "The result is returned.", typeof(SectionDTO))]
        [HttpGet("{sectionId}")]
        public async Task<IActionResult> GetSection([FromRoute] int sectionId)
        {
            return HttpResultMessage.FilteredResult(await sectionService.GetDetails(sectionId));
        }

        [HttpGet]
        public IActionResult GetSections()
        {
            return HttpResultMessage.FilteredResult(sectionService.GetSections());
        }
    }
}
