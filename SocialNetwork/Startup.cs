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
using SocialNetwork.Models.Interfaces;
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

            var dbSection = Configuration.GetSection(nameof(SocialNetworkDatabaseSettings));

            services.Configure<SocialNetworkDatabaseSettings>(dbSection);

            services.AddSingleton<ISocialNetworkDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<SocialNetworkDatabaseSettings>>().Value);

            var mongoDbSettings = dbSection.Get<SocialNetworkDatabaseSettings>();
            services.AddIdentity<ApplicationUser, Role>(op => op.User.AllowedUserNameCharacters = null)
                .AddMongoDbStores<ApplicationUser, Role, Guid>
                (
                    mongoDbSettings.ConnectionString, 
                    mongoDbSettings.DatabaseName
                );

            services.AddSingleton<PublicationService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("account", "Home/",
                    new { Controller = "Home", action = "Index" });
                endpoints.MapControllerRoute("edit", "Home/EditUserPage",
                    new { Controller = "Home", action = "EditUserPage" });
                endpoints.MapControllerRoute("user", "Home/{name}", 
                    new { Controller = "Home", action = "Index" });
                endpoints.MapControllerRoute("people", "People/",
                    new { Controller = "People", action = "Index" });
                endpoints.MapControllerRoute("category", "People/{category}",
                    new { Controller = "People", action = "Index" });
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Publications}/{action=Index}/{id?}");
            });

            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
