using System;
using Producks.Undercutter;
using Producks.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Producks.Repo.ViewModels;

namespace Producks.Repo
{
    public class RepoApi
    {
        private readonly StoreDb _context;
        private List<Producks.Undercutter.Models.Category> UndercuttersCategories;
        private List<Producks.Undercutter.Models.Product> UndercuttersProducts;
        private List<Producks.Undercutter.Models.Brand> UndercuttersBrands;

        public RepoApi(StoreDb context)
        {
            _context = context;
            UndercuttersCategories = UndercuttersAPI.GetCategories().Result;
            UndercuttersProducts = UndercuttersAPI.GetProducts().Result;
            UndercuttersBrands = UndercuttersAPI.GetBrands().Result;
        }

        public IEnumerable<string> GetCategoriesName()
        {
            var catName = (_context.Categories.AsEnumerable().Where(c => c.Active).Select(c => c.Name)).Union(UndercuttersCategories.AsEnumerable().Select(c => c.Name)).Distinct();
            return catName;
        }

        public IEnumerable<string> GetBrandsName()
        {
            var brandName = (_context.Brands.AsEnumerable().Where(b => b.Active).Select(b => b.Name)).Union(UndercuttersBrands.AsEnumerable().Select(c => c.Name)).Distinct();
            return brandName;
        }

        public IEnumerable<ProductRepoVM> GetAllProducts()
        {
            var products = _context.Products.AsEnumerable().Where(p => p.Active)
                .Select(x => new ProductRepoVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    StockLevel = x.StockLevel,
                    Brand = x.Brand.Name,
                    Category = x.Category.Name,
                    Undercutters = false    //shows local database
                }).Union(UndercuttersProducts.AsEnumerable().Select(x => new ProductRepoVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    inStock = x.InStock,
                    Brand = x.BrandName,
                    Category = x.CategoryName,
                    Undercutters = true //shows external database
                }));
            return products;
        }
    }
}
