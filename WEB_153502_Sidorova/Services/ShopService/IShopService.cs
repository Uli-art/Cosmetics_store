using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Models;

namespace WEB_153502_Sidorova.Services.CosmeticsService
{
    public interface IShopService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
