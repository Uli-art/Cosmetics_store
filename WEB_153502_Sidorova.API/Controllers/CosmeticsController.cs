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
using Microsoft.AspNetCore.Authorization;

namespace WEB_153502_Sidorova.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CosmeticsController : ControllerBase
    {
        private ICosmeticsService _cosmeticsService { get; set; }

        private ILogger<CosmeticsController> _logger;
        private string _imagesPath;
        private string? _appUri;

        public CosmeticsController(ICosmeticsService context, IWebHostEnvironment env, IConfiguration configuration, ILogger<CosmeticsController> logger)
        {
            _cosmeticsService = context;
            _logger = logger;
            _imagesPath = Path.Combine(env.WebRootPath, "Images");
            _appUri = configuration.GetSection("appUri").Value;
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
        [Authorize]
        public async Task PutCosmetics(int id, Cosmetics cosmetics)
        {
            await _cosmeticsService.UpdateProductAsync(id, cosmetics);
        }

        // POST: api/Cosmetics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseData<Cosmetics>>> PostCosmetics(Cosmetics cosmetics)
        {
          return await _cosmeticsService.CreateProductAsync(cosmetics);
        }

        // DELETE: api/Cosmetics/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task DeleteCosmetics(int id)
        {
            await _cosmeticsService.DeleteProductAsync(id);
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseData<string>>> PostImage( int id, IFormFile formFile)
        {
            var response = await _cosmeticsService.SaveImageAsync(id, formFile);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        private Task<ResponseData<Cosmetics>> CosmeticsExists(int id)
        {
            return _cosmeticsService.GetProductByIdAsync(id);
        }

    }
}
