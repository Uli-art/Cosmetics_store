using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CosmeticsShop.Domain.Entities;
using WEB_153502_Sidorova.Services.CosmeticsService;

namespace WEB_153502_Sidorova.Areas.Admin
{
    public class CreateModel : PageModel
    {
        private readonly ICosmeticsService _productService;

        public CreateModel(ICosmeticsService productService)
        {
            _productService = productService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Cosmetics Cosmetics { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || Cosmetics == null)
            {
                return Page();
            }

            await _productService.CreateProductAsync(Cosmetics, Image);

            return RedirectToPage("./Index");
        }
    }
}
