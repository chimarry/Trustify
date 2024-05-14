using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Trustify.Backend.FeaturesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableContentController : ControllerBase
    {
        // Read some tabelar data -> GET
        // Add new entry in table -> POST
        // Edit row in table -> PUT
        // Delete entry from table -> DELETE/PUT IS SAFER
        // Sort, filter
        // Find entry in table
    }
}
