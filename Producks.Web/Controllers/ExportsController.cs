using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Producks.Data;
using Producks.Web.Models;

namespace Producks.Web.Controllers
{
    [ApiController]
    public class ExportsController : ControllerBase
    {
        private readonly StoreDb _context;

        public ExportsController(StoreDb context)
        {
            _context = context;
        }

        // GET: api/Brands
        [HttpGet("api/Brands")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _context.Brands
                                       .Select(b => new BrandDto
                                       {
                                           Id = b.Id,
                                           Name = b.Name,
                                           Active = b.Active
                                       })
                                       .ToListAsync();
            return Ok(brands);
        }


        // GET: api/Categories
        [HttpGet("api/Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var Categories = await _context.Categories
                                       .Select(c => new CategoryDto
                                       {
                                           Id = c.Id,
                                           Name = c.Name,
                                           Active = c.Active,
                                           Description = c.Description
                                       })
                                       .ToListAsync();
            return Ok(Categories);
        }

        // GET: api/Products
        [HttpGet("api/Products")]
        public async Task<IActionResult> GetProducts(int? brandId, int? categoryId, int? min, int? max)
        {
            var Products = await _context.Products.Where(p => (p.Price <= max && p.Price >= min) && (p.CategoryId == categoryId || categoryId == null) && (p.BrandId == brandId || brandId == null))
                                       .Select(p => new ProductsDto
                                       {
                                           Id = p.Id,
                                           Name = p.Name,
                                           Active = p.Active,
                                           Description = p.Description,
                                           BrandId = p.BrandId, 
                                           Price = p.Price,
                                           StockLevel = p.StockLevel,
                                           CategoryId = p.CategoryId
                                       })
                                       .ToListAsync();
            return Ok(Products);
        }
    }
}
