using Microsoft.AspNetCore.Http;
using SocialNetwork.Models.Entities;

namespace SocialNetwork.Models.ViewModels
{
    public class UserAndImageViewModel
    {
        public ApplicationUser User { get; set; }
        public IFormFile Image { get; set; }
    }
}
