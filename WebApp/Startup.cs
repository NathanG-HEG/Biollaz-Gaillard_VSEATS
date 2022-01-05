using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BLL;
using BLL.Interfaces;
using BLL.Managers;
using DataAccessLayer.DBAccesses;
using DataAccessLayer.Interfaces;

namespace WebApp
{
    /// <summary>
    /// Auto-generated class
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();

            services.AddScoped<IRestaurantManager, RestaurantManager>();
            services.AddScoped<IRestaurantsDB, RestaurantsDB>();

            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IOrdersDB, OrdersDB>();

            services.AddScoped<IDeliveryAreaManager, DeliveryAreaManager>();
            services.AddScoped<IDeliveryAreasDB, DeliveryAreasDB>();

            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<ICustomersDB, CustomersDB>();

            services.AddScoped<IDishManager, DishManager>();
            services.AddScoped<IDishesDB, DishesDB>();

            services.AddScoped<ICourierManager, CourierManager>();
            services.AddScoped<ICouriersDB, CouriersDB>();

            services.AddScoped<IComposeManager, ComposeManager>();
            services.AddScoped<ICompositionDB, CompositionDB>();

            services.AddScoped<IUtilities, Utilities>();

            services.AddControllersWithViews();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Customer}/{action=Index}/{id?}");
            });
        }
    }
}
