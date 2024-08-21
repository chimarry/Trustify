using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using Trustify.Backend.ImageGeneratorService.Models;

namespace Trustify.Backend.ImageGeneratorService.Controllers
{
    [Route("image-finder")]
    [ApiController]
    public class ImageFinderController : ControllerBase
    {

        private static HttpClient httpClient = new(new HttpClientHandler()
        { ServerCertificateCustomValidationCallback = (a, b, c, d) => true }
        );
   
        [HttpGet]
        public async Task<IActionResult> Find([FromQuery] string search)
        {
            string baseUrl = "https://api.europeana.eu/api/v2/search.json?wskey=turrymeter&query=";
            string url = $"https://api.europeana.eu/api/v2/search.json?wskey=turrymeter&query={search}&type=IMAGE";
            HttpResponseMessage response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            var europeanaResult = JsonConvert.DeserializeObject<EuropeanaResult>(content);
            string imageDownloadUri = string.Empty;
            Item? item = europeanaResult?.Items?.FirstOrDefault(x => x.EdmPreview != null && x.EdmPreview.Length > 0);
            if (item != null)
            {
                imageDownloadUri = item.EdmPreview[0];
                response = await httpClient.GetAsync(imageDownloadUri);
                var byteContent = await response.Content.ReadAsByteArrayAsync();
                var realData = new DownloadImageWrapper
                {
                    FileData = Convert.ToBase64String(byteContent),
                    FileName = "image_finder.jpg"
                };
                return Ok(realData);
            }
            return BadRequest();
        }
    }
}
