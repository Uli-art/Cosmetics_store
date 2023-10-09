using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using WEB_153502_Sidorova.API.Data;

namespace WEB_153502_Sidorova.API.Services.ShopService
{
    public class ShopService : IShopService
    {
        private readonly AppDbContext _context;

        public ShopService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            return new ResponseData<List<Category>>
            {
                Data = await _context.Categories.ToListAsync()
        };
        }
    }
}
