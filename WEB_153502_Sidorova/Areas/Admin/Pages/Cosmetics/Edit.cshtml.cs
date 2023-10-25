using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CosmeticsShop.Domain.Entities;
using WEB_153502_Sidorova.Services.CosmeticsService;
using Microsoft.Identity.Client;

namespace WEB_153502_Sidorova.Areas.Admin
{
    public class EditModel : PageModel
    {
        private readonly ICosmeticsService _productService;

        public EditModel(ICosmeticsService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Cosmetics Cosmetics { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response =  await _productService.GetProductByIdAsync((int)id);
            if (!response.Success)
            {
                return NotFound();
            }
            Cosmetics = response.Data;
            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _productService.UpdateProductAsync(Cosmetics.Id, Cosmetics, Image);

            return RedirectToPage("./Index");
        }

    }
}
