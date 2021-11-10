using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Models.Entities;

namespace SocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        //private UserManager<User> _userManager { get; set; }
        //public AccountController(UserManager<User> userManager)
        //{
        //    _userManager = userManager;
        //}

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            return View();
        }
    }
}
