using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using SocialNetwork.Models.Entities;
using SocialNetwork.Models.ViewModels;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private UserManager<ApplicationUser> _userManager { get; set; }

        public FriendsController(UserManager<ApplicationUser> userManager) => _userManager = userManager;

        public IActionResult Index()
        {
            return View((from u in _userManager.Users.Take(6).ToList()
                        where u.UserName != User.Identity.Name select (UserWithNameAndSubscribersViewModel)u).ToList());
        }
    }
}
