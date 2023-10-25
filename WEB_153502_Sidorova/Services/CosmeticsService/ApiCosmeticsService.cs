using Azure.Core;
using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WEB_153502_Sidorova.Services.CosmeticsService
{
    public class ApiCosmeticsService : ICosmeticsService
    {
        private HttpClient _httpClient;
        private string _pageSize;
        private JsonSerializerOptions _serializerOptions;
        private ILogger<ApiCosmeticsService> _logger;
        private HttpContext _httpContext;
        public ApiCosmeticsService(HttpClient httpClient, IConfiguration configuration,
            ILogger<ApiCosmeticsService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _pageSize = configuration.GetSection("ItemsPerPage").Value;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<ResponseData<Cosmetics>> CreateProductAsync(Cosmetics product, IFormFile? formFile)
        {
            var uri = new Uri(_httpClient.BaseAddress.AbsoluteUri + "Cosmetics");
            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            var response = await _httpClient.PostAsJsonAsync(uri, product, _serializerOptions);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ResponseData<Cosmetics>>(_serializerOptions);
                if (formFile != null) {
                    await SaveImageAsync(data.Data.Id, formFile);
                }
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
                var token = await _httpContext.GetTokenAsync("access_token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                await _httpClient.DeleteFromJsonAsync<Cosmetics>(uri, _serializerOptions);
            }
        }

        public async Task<ResponseData<Cosmetics>> GetProductByIdAsync(int id)
        {
            var uri = new Uri(_httpClient.BaseAddress.AbsoluteUri + $"Cosmetics/{id}");

            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

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
            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

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
                var token = await _httpContext.GetTokenAsync("access_token");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                await _httpClient.PutAsJsonAsync(uri, product, _serializerOptions);
                if (formFile != null)
                {
                    await SaveImageAsync(id, formFile);
                }
            }
        }

        private async Task SaveImageAsync(int id, IFormFile image)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}Cosmetics/{id}")
            };
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(image.OpenReadStream());
            content.Add(streamContent, "formFile", image.FileName);
            request.Content = content;

            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            await _httpClient.SendAsync(request);
        }

}
}
