using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

//@"Server=(localdb)\mssqllocaldb;Database=products;Trusted_Connection=True;ConnectRetryCount=0"

namespace SimpleStore.Models.Product
{
    public class ProductDbContext :DbContext
    {
        //table
        public DbSet<Product> Products { get; set; }


        public ProductDbContext(DbContextOptions<ProductDbContext> options) :base(options)
        {
            
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasIndex(a => a.Name);  
        }
    }
}
