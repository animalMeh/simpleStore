using System;
using System.Collections.Generic;
using SimpleStore.Models.Product;


namespace SimpleStore.Models.Cart
{
    public class CartItem
    {
        public Product.Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
