using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Models;

namespace WEB_153502_Sidorova.API.Services.ShopService
{
    public interface IShopService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();

    }
}
