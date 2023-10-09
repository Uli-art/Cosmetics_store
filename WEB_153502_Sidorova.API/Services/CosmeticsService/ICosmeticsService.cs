using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Models;

namespace WEB_153502_Sidorova.API.Services.CosmeticsService
{
    public interface ICosmeticsService
    {
        public Task<ResponseData<ProductListModel<Cosmetics>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize=3);
        public Task<ResponseData<Cosmetics>> GetProductByIdAsync(int id);
        public Task UpdateProductAsync(int id, Cosmetics product);
        public Task DeleteProductAsync(int id);
        public Task<ResponseData<Cosmetics>> CreateProductAsync(Cosmetics product);
        public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile);
    }

}

