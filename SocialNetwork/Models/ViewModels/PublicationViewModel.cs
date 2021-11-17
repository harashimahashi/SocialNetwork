using SocialNetwork.Models.Entities;
using System.Collections.Generic;

namespace SocialNetwork.Models.ViewModels
{
    public class PublicationViewModel : LikesViewModel
    {
        public Publication Publication { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}
