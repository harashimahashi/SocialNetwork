using SocialNetwork.Models.Entities;

namespace SocialNetwork.Models.ViewModels
{
    public class CommentViewModel : LikesViewModel
    {
        public Comment Comment { get; set; }
    }
}
