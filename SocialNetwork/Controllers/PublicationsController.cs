using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class PublicationsController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult AddPublication() => View();
    }
}
