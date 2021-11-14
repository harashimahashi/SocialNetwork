using SocialNetwork.Models.Entities;

namespace SocialNetwork.Models.ViewModels
{
    public class UserWithNameAndSubscribersViewModel
    {
        public User User { get; set; }

        public string UserName { get; set; }

        public int Subscribers { get; set; }
        public bool Subscribed { get; set; }

        public static implicit operator UserWithNameAndSubscribersViewModel(ApplicationUser user)
        {
            return new UserWithNameAndSubscribersViewModel
            {
                User = user,
                UserName = user.UserName,
                Subscribers = user.Subscribers
            };
        }
    }
}
