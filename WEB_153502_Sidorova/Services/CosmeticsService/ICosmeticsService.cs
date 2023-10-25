using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Models;

namespace WEB_153502_Sidorova.Services.CosmeticsService
{
    public interface ICosmeticsService
    {
        public Task<ResponseData<ProductListModel<Cosmetics>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 4);
        public Task<ResponseData<Cosmetics>> GetProductByIdAsync(int id);
        public Task UpdateProductAsync(int id, Cosmetics product, IFormFile? formFile);
        public Task DeleteProductAsync(int id);
        public Task<ResponseData<Cosmetics>> CreateProductAsync(Cosmetics product, IFormFile? formFile);

    }
}
