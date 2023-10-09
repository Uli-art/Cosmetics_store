﻿using CosmeticsShop.Domain.Entities;
using CosmeticsShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using WEB_153502_Sidorova.API.Data;

namespace WEB_153502_Sidorova.API.Services.CosmeticsService
{

    public class CosmeticsService : ICosmeticsService
    {
        private readonly int _maxPageSize = 30;
        private readonly AppDbContext _context;

        public CosmeticsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseData<Cosmetics>> CreateProductAsync(Cosmetics product)
        {
            try
            {
                await _context.CosmeticsSet.AddAsync(product);
            }
            catch (Exception ex) 
            {
                return new ResponseData<Cosmetics>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
            _context.SaveChanges();
            var response =  new ResponseData<Cosmetics>
            {
                Data = product
            };
            return response;
        }

        public async Task DeleteProductAsync(int id)
        { 
            ResponseData<Cosmetics> response = await GetProductByIdAsync(id);
                
            if (response.Success)
            {
                _context.CosmeticsSet.Remove(response.Data);
            }
            _context.SaveChanges();
        }

        public async Task<ResponseData<Cosmetics>> GetProductByIdAsync(int id)
        {
            Cosmetics product;
            try
            {
                product = await _context.CosmeticsSet.SingleAsync(x => x.Id == id);
            } catch (Exception ex)
            {
                return new ResponseData<Cosmetics>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
            _context.SaveChanges();
            return new ResponseData<Cosmetics>
            {
                Data = product
            };
        }

        public async Task<ResponseData<ProductListModel<Cosmetics>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
        {
            if (pageSize > _maxPageSize)
                pageSize = _maxPageSize;

            var query = _context.CosmeticsSet.AsQueryable();
            var dataList = new ProductListModel<Cosmetics>();

            query = query.Where(d => categoryNormalizedName == null || d.Category.NormalizedName.Equals(categoryNormalizedName));

            var count = query.Count();
            if (count == 0)
            {
                return new ResponseData<ProductListModel<Cosmetics>>
                {
                    Data = dataList
                };
            }

            int totalPages = (int)Math.Ceiling(count / (double)pageSize);
            if (pageNo > totalPages)
                return new ResponseData<ProductListModel<Cosmetics>>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "No such page"
                };

            dataList.Items = await query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
            dataList.CurrentPage = pageNo;
            dataList.TotalPages = totalPages;

            var response = new ResponseData<ProductListModel<Cosmetics>>
            {
                Data = dataList
            };
            return response;
        }

        public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            ResponseData<Cosmetics> response = await GetProductByIdAsync(id);

            if (response.Success)
            {
                if (formFile != null)
                {
                    string path = "/Images/" + formFile.FileName;
                    using (var fileStream = new FileStream("wwwroot/" + path, FileMode.Create))
                    {
                        await formFile.CopyToAsync(fileStream);
                    }
                    Cosmetics product = response.Data;
                    product.Image =  path;
                    await UpdateProductAsync(id, product);
                    _context.SaveChanges();
                    return new ResponseData<string> 
                    { 
                       Data = path
                    };
                }
            }
            return new ResponseData<string>
            {
                Data = null,
                Success = false
            };
        }

        public async Task UpdateProductAsync(int id, Cosmetics product)
        {
            ResponseData<Cosmetics> response = await GetProductByIdAsync(id);

            if (response.Success)
            {
                product.Id = id;  
                _context.CosmeticsSet.Update(product);
            }
            _context.SaveChanges();
        }
    }
}
