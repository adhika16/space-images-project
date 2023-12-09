using webapi.Models;

namespace webapi.Services
{
    public interface INasaApiService
    {
        Task<List<ApodResponse>> GetApodAsync(int count, string apiKey);
    }
}
