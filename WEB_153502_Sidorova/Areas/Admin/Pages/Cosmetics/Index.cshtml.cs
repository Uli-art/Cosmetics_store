using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CosmeticsShop.Domain.Entities;
using WEB_153502_Sidorova.Services.CosmeticsService;

namespace WEB_153502_Sidorova.Areas.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ICosmeticsService _productService;

        public IndexModel(ICosmeticsService productService)
        {
            _productService = productService;
        }

        public IList<Cosmetics> Cosmetics { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var response = await _productService.GetProductListAsync(null);
            if (response.Success)
            {
                Cosmetics = response.Data.Items!;
            }
        }
    }
}
