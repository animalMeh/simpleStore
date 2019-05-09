using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Models.Product;

namespace SimpleStore.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
#region ctorprop
        private ProductDbContext shopContext;
        public ProductController(ProductDbContext shopContext)
        {
            this.shopContext = shopContext;
        }
        #endregion

#region mainview

        [Route("product")]
        [Route("catalog")]
        [Route("~/")]
        public ActionResult Index()
        {
            var products = shopContext.Products.Select(s=>s).ToList();
            return View(products);
        }
        #endregion

        public ActionResult Category(string category)
        {
            var products = shopContext.Products.Where(s => s.Category.ToString() == category).ToList();
            return View("Index", products);
        }


#region apirequest
        [Route("/about")]
        [HttpGet]
        public JsonResult About()
        {
            var a = shopContext.Products.Select(x => x).ToList();
            return new JsonResult(a);
        }
#endregion
    }
}

        //[Route("product")]
        //[Route("catalog/")]
        //[Route("~/")]
        //[HttpPost]
        //public JsonResult Index([FromForm] Product model)
        //{
        //    var a = shopContext.Products.Select(x => x).ToList();
        //    return new JsonResult(a);
        //}