using SocialNetwork.Models.Entities;

namespace SocialNetwork.Models.ViewModels
{
    public class PublicationViewModel
    {
        public Publication Publication { get; set; }

        public int Likes { get; set; }

        public bool IsLiked { get; set; }
    }
}
