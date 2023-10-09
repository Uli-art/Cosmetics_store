using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using WEB_153502_Sidorova.Services.CosmeticsService;

namespace WEB_153502_Sidorova.Services.ShopService
{
    public class ApiShopService : IShopService
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializerOptions;
        private ILogger<ApiCosmeticsService> _logger;
        public ApiShopService(HttpClient httpClient, ILogger<ApiCosmeticsService> logger)
        {
            _httpClient = httpClient;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }

        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}Categories");

            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<List<Category>>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return new ResponseData<List<Category>>
                    {
                        Success = false,
                        ErrorMessage = $"Ошибка: {ex.Message}"
                    };
                }

            }
            _logger.LogError($"-----> Данные не получены от сервера. Error{response.StatusCode.ToString()}");
            return new ResponseData<List<Category>>
            {
                Success = false,
                ErrorMessage = $"Данные не получены от сервера. Error:{response.StatusCode.ToString()}"
            };
        }
    }
}
