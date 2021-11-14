using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models.Entities;
using SocialNetwork.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager { get; set; }

        private SignInManager<ApplicationUser> _signInManager { get; set; }

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) => 
            (_userManager, _signInManager) = (userManager, signInManager);

        public ActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel, string returnurl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = await _userManager.FindByNameAsync(loginModel.Email.Substring(0, loginModel.Email.IndexOf('@')));
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, loginModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnurl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(loginModel.Email), "Login Failed: Invalid Email or Password");
            }

            return View();
        }

        public ActionResult Registration() => View();

        [HttpPost]
        public async Task<ActionResult> Registration(User user, [Required] string password)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = user.Email.Substring(0, user.Email.IndexOf('@')),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, password);
                if (result.Succeeded)
                {
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
