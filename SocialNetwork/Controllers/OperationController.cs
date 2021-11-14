using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class OperationController : Controller
    {
        private UserManager<ApplicationUser> _userManager { get; set; }

        public OperationController(UserManager<ApplicationUser> userManager) => _userManager = userManager;

        public async Task<IActionResult> Subscribe([Required] string name, string returnurl)
        {
            var subscriber = await _userManager.FindByNameAsync(User.Identity.Name);
            var subscribe = await _userManager.FindByNameAsync(name);

            if (subscriber.Subscribed is not null)
            {
                subscriber.Subscribed.Add(subscribe.Id);
            }
            else
            {
                subscriber.Subscribed = new() { subscribe.Id };
            }
            subscribe.Subscribers++;

            var result1 = await _userManager.UpdateAsync(subscriber);
            var result2 = await _userManager.UpdateAsync(subscribe);

            if (result1.Succeeded && result2.Succeeded)
            {
                return Redirect(returnurl);
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Unsubscribe([Required] string name, string returnurl)
        {
            var subscriber = await _userManager.FindByNameAsync(User.Identity.Name);
            var subscribe = await _userManager.FindByNameAsync(name);

            subscriber.Subscribed.Remove(subscribe.Id);
            subscribe.Subscribers--;

            var result1 = await _userManager.UpdateAsync(subscriber);
            var result2 = await _userManager.UpdateAsync(subscribe);

            if (result1.Succeeded && result2.Succeeded)
            {
                return Redirect(returnurl);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
