using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Model;
using SimpleStore.Models.Product;

namespace SimpleStore.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {

        private ProductDbContext shopContext;
        public ProductController(ProductDbContext shopContext)
        {
            this.shopContext = shopContext;
        }

        [Route("product")]
        [Route("catalog")]
        [Route("~/")]
        public ActionResult Index()
        {
            var products = shopContext.Products.Select(s=>s).ToList();
            return View(products);
        }

        public ActionResult Category(string category)
        {
            var products = shopContext.Products.Where(s => s.Category.ToString() == category).ToList();
            return View("Index", products);
        }

        [Route("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}