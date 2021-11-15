using SocialNetwork.Models.Entities;

namespace SocialNetwork.Models.ViewModels
{
    public class UserNameViewModel
    {
        public User User { get; set; }

        public string UserName { get; set; }

        public static implicit operator UserNameViewModel(ApplicationUser user)
        {
            return new() {
                User = user,
                UserName = user.UserName
            };
        }
    }
}
