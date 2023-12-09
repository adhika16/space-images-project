using StackExchange.Redis;
using System.Text.Json;
using webapi.Models;

namespace webapi.Services
{
    public class NasaApiService : INasaApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDatabase _redisDatabase;

        public NasaApiService(IHttpClientFactory httpClientFactory, IConnectionMultiplexer redisConnection)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _redisDatabase = redisConnection.GetDatabase();

        }

        public async Task<List<ApodResponse>> GetApodAsync(int count, string apiKey)
        {
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("ddMMyyyy-HH");
            string cacheKey = $"apod_data_{formattedDateTime}";
            // Check if data is in Redis cache
            var cachedData = await _redisDatabase.StringGetAsync(cacheKey);
            if (!cachedData.IsNull)
            {
                return JsonSerializer.Deserialize<List<ApodResponse>>(cachedData);
            }

            var httpClient = _httpClientFactory.CreateClient();
            var apiUrl = $"https://api.nasa.gov/planetary/apod?count={count}&api_key={apiKey}";

            var response = await httpClient.GetFromJsonAsync<List<ApodResponse>>(apiUrl);

            // Store data in Redis with a 60-minute expiration
            await _redisDatabase.StringSetAsync(cacheKey, JsonSerializer.Serialize(response), TimeSpan.FromMinutes(60));

            return response;
        }
    }
}
