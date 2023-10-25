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
    public class DeleteModel : PageModel
    {
        private readonly ICosmeticsService _productService;

        public DeleteModel(ICosmeticsService productService)
        {
            _productService = productService;
        }

        [BindProperty]
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

            Cosmetics = response.Data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await _productService.GetProductByIdAsync((int)id);

            if (response.Success)
            {
                await _productService.DeleteProductAsync((int)id);
            }

            return RedirectToPage("./Index");
        }
    }
}
