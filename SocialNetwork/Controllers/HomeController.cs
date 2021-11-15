using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models.Entities;
using SocialNetwork.Models.ViewModels;
using System.IO;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> _userManager { get; set; }

        public HomeController(UserManager<ApplicationUser> userManager) => _userManager = userManager;

        public async Task<IActionResult> Index(string name)
        {
            UserNameAndSubscribersViewModel ret;
            if (name is null)
            {
                ret = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            else
            {
                var current = await _userManager.FindByNameAsync(User.Identity.Name);
                var user = await _userManager.FindByNameAsync(name);

                if (user is null)
                {
                    return View("Error 404");
                }

                ret = user;
                ret.Subscribed = current.Subscribed.Contains(user.Id) ? true : false;
            }
            return View(ret);
        }

        public async Task<ActionResult> EditUserPage()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(new UserAndImageViewModel { User = user });
        }

        [HttpPost]
        public async Task<ActionResult> EditUserPage(UserAndImageViewModel updateUser)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            user.FirstName = updateUser.User.FirstName;
            user.LastName = updateUser.User.LastName;
            user.Residence = updateUser.User.Residence;
            user.BirthDate = updateUser.User.BirthDate.AddHours(2);
            user.Interests = updateUser.User.Interests;
            user.Image = updateUser.Image is not null ? ConvertImage(updateUser.Image) : null;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new UserAndImageViewModel { User = user });
        }

        private byte[] ConvertImage(IFormFile formFile)
        {
            byte[] imageData;
            using (var binaryReader = new BinaryReader(formFile.OpenReadStream()))
                imageData = binaryReader.ReadBytes((int)formFile.Length);
            return imageData;
        }
    }
}