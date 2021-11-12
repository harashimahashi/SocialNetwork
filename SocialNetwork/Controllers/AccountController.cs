using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SocialNetwork.Models.Entities;

namespace SocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager { get; set; }
        private SignInManager<ApplicationUser> _signInManager { get; set; }
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Required][EmailAddress] string email, [Required] string password, string returnurl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = await _userManager.FindByNameAsync(email);
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnurl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(email), "Login Failed: Invalid Email or Password");
            }

            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registration(User user, [Required] string password)
        {
            if (ModelState.IsValid) {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, password);
                if (result.Succeeded) {
                    ViewBag.Message = "User Created Successfully";
                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(appUser, password, false, false);
                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Publications");
                    }
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
