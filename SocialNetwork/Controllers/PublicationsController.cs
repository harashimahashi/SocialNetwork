using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    public class PublicationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
