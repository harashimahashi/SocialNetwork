using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Models.Entities;

namespace SocialNetwork.Models
{
    public static class IdentitySeedData
    {
        private const string maryEmail = "maria@gmail.com";
        private const string maryPass = "Secret123_";
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            ApplicationUser user = await userManager.FindByNameAsync(maryEmail);
            if (user == null)
            {
                var res = await userManager.CreateAsync(
                    new ApplicationUser
                    {
                        FirstName = "Мария",
                        LastName = "Литвинова",
                        Residence = "Москва",
                        BirthDate = new(1996, 8, 17),
                        UserName = maryEmail,
                        Interests = @"Lorem ipsum dolor sit amet consectetur adipisicing elit. Alias ipsum soluta dolor magni maiores impedit, 
                                      temporibus saepe aspernatur totam, voluptate consequuntur. Animi nisi iusto rerum molestias voluptatem
                                      dicta vero eius?"
                    }, maryPass
                );
            }
        }
    }
}
