using SocialNetwork.Models.Entities;

namespace SocialNetwork.Models.ViewModels
{
    public class UserNameAndSubscribersViewModel : UserNameViewModel
    {
        public int Subscribers { get; set; }

        public bool Subscribed { get; set; }

        public static implicit operator UserNameAndSubscribersViewModel(ApplicationUser user)
        {
            return new UserNameAndSubscribersViewModel
            {
                User = user,
                UserName = user.UserName,
                Subscribers = user.Subscribers
            };
        }
    }
}
