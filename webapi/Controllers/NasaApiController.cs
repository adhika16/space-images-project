using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using webapi.Models;
using webapi.Services;
using webapi.Utils;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NasaApiController : ControllerBase
    {
        private readonly INasaApiService _nasaApiService;
        private readonly NasaApiConfigs _configs;

        public NasaApiController(INasaApiService nasaApiService, NasaApiConfigs configs)
        {
            _nasaApiService = nasaApiService ?? throw new ArgumentNullException(nameof(nasaApiService));
            _configs = configs ?? throw new ArgumentNullException(nameof(configs));
        }

        [HttpGet("apod")]
        public async Task<ActionResult<List<ApodResponse>>> GetApod()
        {
            try
            {
                var apiKey = _configs.ApiKey;
                var count = _configs.Count;
                var result = await _nasaApiService.GetApodAsync(count, apiKey);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
