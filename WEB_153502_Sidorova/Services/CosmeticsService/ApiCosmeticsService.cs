using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace WEB_153502_Sidorova.Services.CosmeticsService
{
    public class ApiCosmeticsService : ICosmeticsService
    {
        private HttpClient _httpClient;
        private string _pageSize;
        private JsonSerializerOptions _serializerOptions;
        private ILogger<ApiCosmeticsService> _logger;
        public ApiCosmeticsService(HttpClient httpClient, IConfiguration configuration,
            ILogger<ApiCosmeticsService> logger)
        {
            _httpClient = httpClient;
            _pageSize = configuration.GetSection("ItemsPerPage").Value;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }

        public async Task<ResponseData<Cosmetics>> CreateProductAsync(Cosmetics product, IFormFile? formFile)
        {
            var uri = new Uri(_httpClient.BaseAddress.AbsoluteUri + "Cosmetics");

            var response = await _httpClient.PostAsJsonAsync(uri, product, _serializerOptions);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ResponseData<Cosmetics>>(_serializerOptions);
                return data;
            }
            else
            {
                _logger.LogError($"-----> object not created. Error{response.StatusCode.ToString()}");
            }

            return new ResponseData<Cosmetics>
            {
                Success = false,
                ErrorMessage = $"Объект не добавлен. Error{response.StatusCode.ToString()}"
            };
        }

        public async Task DeleteProductAsync(int id)
        {
            var uri = new Uri(_httpClient.BaseAddress.AbsoluteUri + $"Cosmetics/{id}");
            ResponseData<Cosmetics> isFind = await GetProductByIdAsync(id);
            if (isFind.Success)
            {
                await _httpClient.DeleteFromJsonAsync<Cosmetics>(uri, _serializerOptions);
            }
        }

        public async Task<ResponseData<Cosmetics>> GetProductByIdAsync(int id)
        {
            var uri = new Uri(_httpClient.BaseAddress.AbsoluteUri + $"Cosmetics/{id}");

            var response = await _httpClient.GetAsync(new Uri(uri.ToString()));
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ResponseData<Cosmetics>>(_serializerOptions);
                return data;
            }
            else
            {
                _logger.LogError($"-----> object not found. Error{response.StatusCode.ToString()}");
            }

            return new ResponseData<Cosmetics>
            {
                Success = false,
                ErrorMessage = $"object not found. Error{response.StatusCode.ToString()}"
            };
        }

        public async Task<ResponseData<ProductListModel<Cosmetics>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}Cosmetics/");

            if (categoryNormalizedName != null)
            {
                urlString.Append($"{categoryNormalizedName}/");
            };

            if (pageNo > 1)
            {
                urlString.Append($"page{pageNo}");
            };

            if (!_pageSize.Equals("3"))
            {
                urlString.Append(QueryString.Create("pageSize", _pageSize));
            }

            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<ProductListModel<Cosmetics>>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return new ResponseData<ProductListModel<Cosmetics>>
                    {
                        Success = false,
                        ErrorMessage = $"Ошибка: {ex.Message}"
                    };
                }
            }
            _logger.LogError($"-----> Данные не получены от сервера. Error{response.StatusCode.ToString()}");
            return new ResponseData<ProductListModel<Cosmetics>>
            {
                Success = false,
                ErrorMessage = $"Данные не получены от сервера. Error:{response.StatusCode.ToString()}"
            };
        }

        public async Task UpdateProductAsync(int id, Cosmetics product, IFormFile? formFile)
        {
            var uri = new Uri(_httpClient.BaseAddress.AbsoluteUri + $"Cosmetics/{id}");
            ResponseData<Cosmetics> isFind = await GetProductByIdAsync(id);
            if (isFind.Success)
            {
                await _httpClient.PutAsJsonAsync(uri, product, _serializerOptions);
            }
        }
    }
}
