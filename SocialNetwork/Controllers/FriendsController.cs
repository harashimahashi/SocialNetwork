using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Models.Entities;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private UserManager<ApplicationUser> _userManager { get; set; }

        public FriendsController(UserManager<ApplicationUser> userManager) => _userManager = userManager;

        public IActionResult Index()
        {
            return View(_userManager.Users.Take(6).ToList().Select(el => (User)el).ToList());
        }
    }
}
