using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleStore.Models.Product;
using SimpleStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using SimpleStore.Models.Product.ProductDbProvider;

namespace SimpleStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var c = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")), 
                ServiceLifetime.Singleton);
            services.AddMvc();
            services.AddHttpContextAccessor();
            services.AddSession();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ProductDbContext serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseStaticFiles();

            app.UseMvc(routes =>
           {
               routes.MapRoute(
                   name: "dafault",
                   template: " {controller=Product}/{action=Index}/{category?}"
                   );

           });
            app.UseExceptionHandler("/Product/error");

            
            var dbContext = app.ApplicationServices.GetService(typeof(ProductDbContext)) as ProductDbContext;
            dbContext.Database.EnsureCreated();
            if(!dbContext.Products.Any())
            {
                ProductDbProvider.GenerateTable(dbContext);
            }

        }
    }
}