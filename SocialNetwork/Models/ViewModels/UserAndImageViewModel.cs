using Microsoft.AspNetCore.Http;
using SocialNetwork.Models.Entities;

namespace SocialNetwork.Models.ViewModels
{
    public class UserAndImageViewModel
    {
        public User User { get; set; }

        public IFormFile Image { get; set; }
    }
}
