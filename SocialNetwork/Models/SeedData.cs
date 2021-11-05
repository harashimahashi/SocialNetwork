using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Services;

namespace SocialNetwork.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            UserService user = app.ApplicationServices.CreateScope().ServiceProvider.GetService<UserService>();

            if (user.Get().Count == 0)
            {
                user.Create(
                    new Entities.User
                    {
                        Nickname = "MariLi",
                        Residence = "Москва",
                        BirthDate = new(1996, 8, 17),
                        Email = "maria@gmail.com",
                        Interests = @"Lorem ipsum dolor sit amet consectetur adipisicing elit. Alias ipsum soluta dolor magni maiores impedit, 
                                      temporibus saepe aspernatur totam, voluptate consequuntur. Animi nisi iusto rerum molestias voluptatem
                                      dicta vero eius?"
                    }
                );
            }
        }
    }
}
