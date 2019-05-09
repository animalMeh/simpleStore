using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using SimpleStore.Models.Cart;
using SimpleStore.Session;
using Microsoft.AspNetCore.Http;
using SimpleStore.Models.Product;

namespace SimpleStore.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private ProductDbContext shopContext;
        HttpContext httpContext;
        private List<CartItem> currentSessionCart;

        public CartController(IHttpContextAccessor acc, ProductDbContext shopContext)
        {
            this.httpContext = acc.HttpContext;
         //   var items = SessitonSaver.GetObjectFromJson<Dictionary<Product, int>>(context.Session, "cart");
            this.currentSessionCart = SessitonSaver.GetObjectFromJson<List<CartItem>>(this.httpContext.Session, "cart") ?? new List<CartItem>();
            this.shopContext = shopContext;
           
        }

        [Route("cart")]
        public IActionResult Index()
        {
            ViewBag.cart = this.currentSessionCart;
            return View();
        }

        [Route("add")]
        [HttpPost]
        public int Add([FromBody]string id)
        {
            var selectedProduct = shopContext.Products.Find(int.Parse(id));
            if(selectedProduct!= null)
            {
                var itemInCart = currentSessionCart.Where(s => s.Product.Id == int.Parse(id)).ToList();
                if (itemInCart.Count > 0)
                {
                    itemInCart.FirstOrDefault().Quantity++;
                }
                else
                {
                    currentSessionCart.Add(new CartItem() { Product = selectedProduct, Quantity = 1 });
                }
            }

            SessitonSaver.SetObjectAsJson(this.httpContext.Session, "cart",  this.currentSessionCart);
            return this.currentSessionCart.Sum(s => s.Quantity);
        }

        [Route("remove")]
        [HttpPost]
        public int Remove([FromBody]string id)
        {
            var selectedProduct = shopContext.Products.Find(int.Parse(id));
            if (selectedProduct != null && currentSessionCart.Count > 0)
            {
                var item = currentSessionCart.Where(s => s.Product.Id == int.Parse(id)).ToList().FirstOrDefault();
                if (item != null)
                {
                    item.Quantity--;
                    if(item.Quantity == 0)
                    {
                        currentSessionCart.Remove(item);
                    }
                }
            }

            SessitonSaver.SetObjectAsJson(this.httpContext.Session, "cart", this.currentSessionCart);
            return this.currentSessionCart.Sum(s => s.Quantity);
        }

        [Route("count")]
        [HttpGet]
        public int Remove()
        {
            return this.currentSessionCart.Sum(s => s.Quantity);
        }
    }

}