using ShopLite.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ShopLite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopLiteDBEntities db = new ShopLiteDBEntities();

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

            ViewBag.Keyword = keyword;
            ViewBag.CategoryId = categoryId;
            ViewBag.Categories = db.Categories.Where(c => c.IsActive).ToList();

            return View(products.OrderBy(p => p.ProductId).ToList());
        }

        //public ActionResult Catalog(string keyword, int? categoryId)
        //{
        //    var products = db.Products
        //        .Include("Category")
        //        .Where(p => p.IsAvailable);

        //    if (!string.IsNullOrWhiteSpace(keyword))
        //    {
        //        products = products.Where(p => p.ProductName.Contains(keyword));
        //    }

        //    if (categoryId.HasValue)
        //    {
        //        products = products.Where(p => p.CategoryId == categoryId.Value);
        //    }

        //    ViewBag.Title = "Danh mục sản phẩm";
        //    ViewBag.Keyword = keyword;
        //    ViewBag.CategoryId = categoryId;
        //    ViewBag.Categories = db.Categories.Where(c => c.IsActive).ToList();

        //    return View(products.OrderBy(p => p.ProductId).ToList());
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
