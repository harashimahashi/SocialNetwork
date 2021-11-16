using SocialNetwork.Models.Entities;
using System.Collections.Generic;

namespace SocialNetwork.Models.ViewModels
{
    public class PublicationsViewModel
    {
        public List<PublicationViewModel> Publications { get; set; }

        public User Current { get; set; }
    }
}
