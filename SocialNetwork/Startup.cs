using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SocialNetwork.Models;
using SocialNetwork.Models.Entities;

namespace SocialNetwork
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //services.Configure<SocialNetworkDatabaseSettings>(
            //    Configuration.GetSection(nameof(SocialNetworkDatabaseSettings)));

            //services.AddSingleton<ISocialNetworkDatabaseSettings>(sp =>
            //    sp.GetRequiredService<IOptions<SocialNetworkDatabaseSettings>>().Value);

            var mongoDbSettings = Configuration.GetSection(nameof(SocialNetworkDatabaseSettings)).Get<SocialNetworkDatabaseSettings>();
            services.AddIdentity<ApplicationUser, Role>(op => op.User.AllowedUserNameCharacters = null)
                .AddMongoDbStores<ApplicationUser, Role, Guid>
                (
                    mongoDbSettings.ConnectionString, 
                    mongoDbSettings.DatabaseName
                );

            //services.AddSingleton<UserService>();
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("user", "Home/",
                    new { Controller = "Home", action = "Index" });
                endpoints.MapControllerRoute("user", "Home/EditUserPage",
                    new { Controller = "Home", action = "EditUserPage" });
                endpoints.MapControllerRoute("user", "Home/{name}", 
                    new { Controller = "Home", action = "Index" });
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Publications}/{action=Index}/{id?}");
            });

            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
