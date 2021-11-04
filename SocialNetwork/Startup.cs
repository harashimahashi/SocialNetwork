using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SocialNetwork.Models;
using SocialNetwork.Services;

namespace SocialNetwork
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.Configure<SocialNetworkDatabaseSettings>(
                Configuration.GetSection(nameof(SocialNetworkDatabaseSettings)));

            services.AddSingleton<ISocialNetworkDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<SocialNetworkDatabaseSettings>>().Value);

            services.AddSingleton<UserService>();

            // Добавление служб репозиториев
            //services.AddScoped<IBookRepository, BookRepository>();
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IOrderRepository, OrderRepository>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();
            //app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Стандартный путь "{controller=Home}/{action=Index}/{id?}"
                endpoints.MapDefaultControllerRoute();
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
