using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json;
using Trustify.Backend.ImageGeneratorService.Models;

namespace Trustify.Backend.ImageGeneratorService.Controllers
{
    [Route("image-finder")]
    [ApiController]
    public class ImageFinderController(IOptions<EuropeanaConfig> config) : ControllerBase
    {
        public EuropeanaConfig Config { get; set; } = config.Value;

        private static readonly HttpClient httpClient = new(new HttpClientHandler()
        { ServerCertificateCustomValidationCallback = (a, b, c, d) => true }
        );

        [HttpGet]
        public async Task<IActionResult> Find([FromQuery] string search)
        {
            string url = $"{Config.BaseUrl}?{Config.KeyParam}={Config.ApiKey}&{Config.SearchParam}={search}&type=IMAGE";
            HttpResponseMessage response = await httpClient.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();

            var europeanaResult = JsonConvert.DeserializeObject<EuropeanaResult>(content);
            Item? item = europeanaResult?.Items?.FirstOrDefault(x => x.EdmPreview != null && x.EdmPreview.Length > 0);
            if (item != null)
            {
                string imageDownloadUri =  item.EdmPreview[0];
                DownloadImageWrapper wrapper = await DownloadImage(imageDownloadUri);
                return Ok(wrapper);
            }
            return BadRequest();
        }

        private async Task<DownloadImageWrapper> DownloadImage(string downloadUri)
        {
            var response = await httpClient.GetAsync(downloadUri);
            var byteContent = await response.Content.ReadAsByteArrayAsync();
            var realData = new DownloadImageWrapper
            {
                FileData = Convert.ToBase64String(byteContent),
                FileName = "image_finder.jpg"
            };
            return realData;
        }
    }
}
