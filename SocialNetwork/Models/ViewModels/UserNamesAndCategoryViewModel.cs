using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Models.Entities;

namespace SocialNetwork.Models.ViewModels
{
    public class UserNamesAndCategoryViewModel
    {
        public List<UserNameViewModel> UserNames { get; set; }

        public string Category { get; set; }
    }
}
