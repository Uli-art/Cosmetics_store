using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Models;

namespace WEB_153502_Sidorova.Services.CosmeticsService
{
    public class MemoryShopService : IShopService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
                new Category {Id=1, Name="Для лица",
                NormalizedName="face"},
                new Category {Id=2, Name="Для тела",
                NormalizedName="body"},
                new Category {Id=3, Name="Для волос",
                NormalizedName="hair"}
            };
            var result = new ResponseData<List<Category>>();
            result.Data = categories;
            return Task.FromResult(result);
        }
    }
}
