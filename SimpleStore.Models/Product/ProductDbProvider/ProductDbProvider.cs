using Newtonsoft.Json;
using Json.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using SimpleStore.Models.Enums;

namespace SimpleStore.Models.Product.ProductDbProvider
{
    public class ProductDbProvider
    { 
        public static void GenerateTable(ProductDbContext context)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var path2file = currentDirectory + "\\Model\\products.json";
            
            using (StreamReader reader = new StreamReader(path2file))
            {
                using (JsonTextReader r = new JsonTextReader(reader))
                {
                    JArray o2 = (JArray)JToken.ReadFrom(r);
                    foreach (var product in o2)
                    {
                        JObject temp = (JObject)product;
                        context.Products.Add(new Product()
                        {
                            Name = temp["name"].ToString(),
                            Category = (ProductCategory)int.Parse(temp["category"].ToString()),
                            Image =  temp["image"].ToString(),
                            Price = double.Parse(temp["price"].ToString())
                        });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
