using CosmeticsShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_153502_Sidorova.Services.CosmeticsService;

namespace WEB_153502_Sidorova.Controllers
{
    public class Cosmetics : Controller
    {
        public IShopService _shopService = new MemoryShopService();
        public ICosmeticsService _service;
        private IConfiguration _config;
        public Cosmetics(IShopService _shopService, ICosmeticsService _service) 
        {
            _shopService = new MemoryShopService();
            _service = new MemoryCosmeticsService(_config, _shopService);
        }
        public async Task<IActionResult> Index(string? category, int pageNo = 1)
        {
            var listOfCategories = await _shopService.GetCategoryListAsync();
            ViewBag.Categories = listOfCategories.Data;

            ViewData["currentCategory"] = category!=null ? listOfCategories.Data?.Find(item=> item.NormalizedName == category).Name : "Все";

            _service = new MemoryCosmeticsService(_config, _shopService);
            var productResponse = await _service.GetProductListAsync(category, pageNo) ;
            if (!productResponse.Success)
                return NotFound(productResponse.ErrorMessage);
            return View(productResponse.Data);
        }
    }
}
