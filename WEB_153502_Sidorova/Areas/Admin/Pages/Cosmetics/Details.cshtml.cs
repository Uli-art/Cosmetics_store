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
    public class DetailsModel : PageModel
    {
        private readonly ICosmeticsService _productService;

        public DetailsModel(ICosmeticsService productService)
        {
            _productService = productService;
        }

      public Cosmetics Cosmetics { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _productService.GetProductByIdAsync((int)id);
            if (!response.Success)
            {
                return NotFound();
            }
            else 
            {
                Cosmetics = response.Data;
            }
            return Page();
        }
    }
}
