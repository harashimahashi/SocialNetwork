using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models.Entities;
using SocialNetwork.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private UserManager<ApplicationUser> _userManager { get; set; }

        public PeopleController(UserManager<ApplicationUser> userManager) => _userManager = userManager;

        public async Task<IActionResult> Index(string category) {

            var current = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(new UserNamesAndCategoryViewModel() { 
                UserNames = (from u in _userManager.Users.ToList()
                             where ((u.UserName != User.Identity.Name) && (category == "Subscribers" ? u.Subscribed.Contains(current.Id) : true))
                             select (UserNameViewModel)u).ToList(), 
                Category = category ?? "All" 
            });
        }
    }
}
