using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Net;
using System.Net.Http;
using Producks.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Producks.Undercutter;
using Producks.Web.ViewModels;
using Producks.Repo;
using Producks.Repo.ViewModels;

namespace Producks.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreDb _context;
        private IEnumerable<string> repoBrands;
        private IEnumerable<string> repoCategories;
        //private List<Producks.Undercutter.Models.Category> UndercuttersCategories;
        //private List<Producks.Undercutter.Models.Product> UndercuttersProducts;
        //private List<Producks.Undercutter.Models.Brand> UndercuttersBrands;
        public StoreController(StoreDb context)
        {
            _context = context;
            RepoApi rp = new RepoApi(context);
            repoBrands = rp.GetBrandsName();
            repoCategories = rp.GetCategoriesName();
            //UndercuttersCategories = UndercuttersAPI.GetCategories().Result;
            //UndercuttersProducts = UndercuttersAPI.GetProducts().Result;
            //UndercuttersBrands = UndercuttersAPI.GetBrands().Result;
        }

        /*
        public ActionResult Index()
        {
            return View(_context.Categories.Where(c => _context.Products.FirstOrDefault(s => s.CategoryId == c.Id && s.Active) != null).ToList());
        }
        */
        public ActionResult Index(string catName = null, string brandName = null)
        {
            ViewBag.Categories = new SelectList(repoCategories);
            ViewBag.Brands = new SelectList(repoBrands);

            RepoApi rp = new RepoApi(_context);
            var products = rp.GetAllProducts();
            



            if (catName !=null && catName != "")
            {
                products = products.Where(p => p.Category == catName);
            }

            if (brandName != null && brandName != "")
            {
                products = products.Where(p => p.Brand == brandName);
            }

            return View(products);
            //return View(_context.Categories.Where(c => _context.Products.FirstOrDefault(s => s.CategoryId == c.Id && s.Active) != null).ToList());
        }
        public ActionResult Display(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            Producks.Data.Category category = _context.Categories.Find(id);
            
            if (category == null)
            {
                return new NotFoundResult();
            }

            var products = _context.Products.Where(s => s.CategoryId == category.Id && s.Active).ToList();
            return View(products);
        }
    }
}