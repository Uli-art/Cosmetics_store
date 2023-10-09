using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CosmeticsShop.Domain.Entities;
using WEB_153502_Sidorova.API.Data;
using CosmeticsShop.Domain.Models;
using WEB_153502_Sidorova.API.Services.CosmeticsService;
using WEB_153502_Sidorova.API.Services.ShopService;

namespace WEB_153502_Sidorova.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CosmeticsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private CosmeticsService _cosmeticsService { get; set; }
        public CosmeticsController(AppDbContext context)
        {
            _context = context;
            _cosmeticsService = new CosmeticsService(_context);
        }

        // GET: api/Cosmetics
        [HttpGet]
        [HttpGet("page{pageNo}")]
        [HttpGet("{category:alpha?}")]
        [HttpGet("{category:alpha?}/page{pageNo}")]
        public async Task<ActionResult<ResponseData<Cosmetics>>> GetCosmeticsSet(string? category, int pageNo = 1, int pageSize = 3)
        {
            return Ok(await _cosmeticsService.GetProductListAsync(
                category,
                pageNo,
                pageSize));
        }

        // GET: api/Cosmetics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseData<Cosmetics>>> GetCosmetics(int id)
        {
          return Ok( await _cosmeticsService.GetProductByIdAsync(id));
        }

        // PUT: api/Cosmetics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task PutCosmetics(int id, Cosmetics cosmetics)
        {
            await _cosmeticsService.UpdateProductAsync(id, cosmetics);
        }

        // POST: api/Cosmetics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResponseData<Cosmetics>>> PostCosmetics(Cosmetics cosmetics)
        {
          return await _cosmeticsService.CreateProductAsync(cosmetics);
        }

        // DELETE: api/Cosmetics/5
        [HttpDelete("{id}")]
        public async Task DeleteCosmetics(int id)
        {
            await _cosmeticsService.DeleteProductAsync(id);
        }

        private Task<ResponseData<Cosmetics>> CosmeticsExists(int id)
        {
            return _cosmeticsService.GetProductByIdAsync(id);
        }
    }
}
