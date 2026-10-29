using ShopLite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer.Symbols;

namespace ShopLite.Controllers
{
    public class ProductController : Controller
    {
        ShopLiteDBEntities db = new ShopLiteDBEntities();
        // GET: Product
        public ActionResult Index(string keyword, int? categoryId)
        {
            var products = db.Products
                .Include("Category")
                .Where(p => p.IsAvailable);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                products = products.Where(p => p.ProductName.Contains(keyword));
            }

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            ViewBag.Title = "Danh mục sản phẩm";
            ViewBag.Keyword = keyword;
            ViewBag.CategoryId = categoryId;
            ViewBag.Categories = db.Categories.Where(c => c.IsActive).ToList();

            return View(products.OrderBy(p => p.ProductId).ToList());
        }

        public ActionResult 
    }
}