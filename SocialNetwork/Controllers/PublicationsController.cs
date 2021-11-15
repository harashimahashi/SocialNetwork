using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Services;
using SocialNetwork.Models.Entities;
using SocialNetwork.Models.ViewModels;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class PublicationsController : Controller
    {
        private UserManager<ApplicationUser> _userManager { get; set; }

        private readonly PublicationService _publicationService;

        public PublicationsController(UserManager<ApplicationUser> userManager, PublicationService publicationService) 
            => (_userManager, _publicationService) = (userManager, publicationService);

        public async Task<IActionResult> Index()
        {
            var current = await _userManager.FindByNameAsync(User.Identity.Name);
            List<Guid> ids = new() { current.Id };
            ids.AddRange(current.Subscribed);

            var pubs = await _publicationService.GetPublicationsByOwnerIdsAsync(ids);
            pubs.Sort((el1, el2) => Convert.ToInt32(el1.Created < el2.Created));

            List<ApplicationUser> users = new() { current };
            for (int i = 1; i < ids.Count; i++) {
                users.Add(await _userManager.FindByIdAsync(ids[i].ToString()));
            }

            return View(pubs.Select(el => {
                var pub = (Publication)el;
                pub.Owner = (from u in users where u.Id == el.Owner select (User)u).First();

                return pub;
            }).ToList());
        }

        public IActionResult AddPublication() => View();

        [HttpPost]
        public async Task<IActionResult> AddPublication(PublicationAndImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var current = await _userManager.FindByNameAsync(User.Identity.Name);

                ApplicationPublication appPub = new() {
                    Owner = current.Id,
                    Created = DateTime.Now,
                    Heading = model.Publication.Heading,
                    Content = model.Publication.Content,
                    Image = model.Image is not null ? ConvertImage(model.Image) : null
                };
                await _publicationService.CreateAsync(appPub);

                return RedirectToAction("Index");
            }

            return View();
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
