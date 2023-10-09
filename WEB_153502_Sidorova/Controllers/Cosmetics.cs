using CosmeticsShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_153502_Sidorova.Services.CosmeticsService;

namespace WEB_153502_Sidorova.Controllers
{
    public class Cosmetics : Controller
    {
        public IShopService _shopService;
        public ICosmeticsService _service;
        private IConfiguration _config;
        public Cosmetics(IShopService shopService, ICosmeticsService service) 
        {
            _shopService = shopService;
            _service = service;
        }
        public async Task<IActionResult> Index(string? category, int pageNo = 1)
        {
            var listOfCategories = await _shopService.GetCategoryListAsync();
            ViewBag.Categories = listOfCategories.Data;

            ViewData["currentCategory"] = category!=null ? listOfCategories.Data?.Find(item=> item.NormalizedName == category).Name : "Все";

            var productResponse = await _service.GetProductListAsync(category, pageNo) ;
            if (!productResponse.Success)
                return NotFound(productResponse.ErrorMessage);
            return View(productResponse.Data);
        }
    }
}
