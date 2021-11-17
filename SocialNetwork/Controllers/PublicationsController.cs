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
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class PublicationsController : Controller
    {
        private UserManager<ApplicationUser> _userManager { get; set; }

        private readonly PublicationService _publicationService;

        private readonly CommentService _commentService;

        public PublicationsController(UserManager<ApplicationUser> userManager, PublicationService publicationService, CommentService commentService) 
            => (_userManager, _publicationService, _commentService) = (userManager, publicationService, commentService);

        public async Task<IActionResult> Index()
        {
            var current = await _userManager.FindByNameAsync(User.Identity.Name);
            List<Guid> ids = new() { current.Id };
            ids.AddRange(current.Subscribed);

            var pubs = await _publicationService.GetByOwnerIdsAsync(ids);
            pubs.Sort((el1, el2) => Convert.ToInt32(el1.Created < el2.Created));

            List<ApplicationUser> users = new() { current };
            for (int i = 1; i < ids.Count; i++) {
                users.Add(await _userManager.FindByIdAsync(ids[i].ToString()));
            }

            return View(new PublicationsViewModel
            {
                Publications = (await Task.WhenAll(pubs.Select(async el =>
                {
                    var pub = (Publication)el;
                    pub.Owner = (from u in users where u.Id == el.Owner select (User)u).First();
                    var coms = await _commentService.GetByPublicationIdAsync(el.Id);
                    coms.Sort((el1, el2) => Convert.ToInt32(el1.Created > el2.Created));

                    return new PublicationViewModel {
                        Publication = pub,
                        Likes = el.Liked.Count,
                        IsLiked = el.Liked.Contains(current.Id),
                        Comments = (await Task.WhenAll(coms.Select(async c => {
                            var com = (Comment)c;
                            var owner = users.Find(u => u.Id == c.Owner);
                            if (owner is null)
                            {
                                com.Owner = await _userManager.FindByIdAsync(c.Owner.ToString());
                            }
                            else {
                                com.Owner = owner;
                            }

                            return new CommentViewModel
                            {
                                Comment = com,
                                Likes = c.Liked.Count,
                                IsLiked = c.Liked.Contains(current.Id)
                            };
                        }))).ToList()
                };
                }))).ToList(),
                Current = current
            });
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

        [HttpPost]
        public async Task<IActionResult> Like([Required] string type, [Required] string id)
        {
            var current = await _userManager.FindByNameAsync(User.Identity.Name);

            if (type == "publication")
            {
                await _publicationService.UpdateOneAsync(id, "Liked", current.Id);
            }
            else
            {
                await _commentService.UpdateOneAsync(id, "Liked", current.Id);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Unlike([Required] string type, [Required] string id)
        {
            var current = await _userManager.FindByNameAsync(User.Identity.Name);

            if (type == "publication")
            {
                var pub = await _publicationService.GetByIdAsync(id);
                pub.Liked.Remove(current.Id);
                await _publicationService.UpdateOneAsync(id, pub);
            }
            else
            {
                var com = await _commentService.GetByIdAsync(id);
                com.Liked.Remove(current.Id);
                await _commentService.UpdateOneAsync(id, com);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([Required] string publication, string content)
        {
            var current = await _userManager.FindByNameAsync(User.Identity.Name);

            ApplicationComment com = new() {
                Owner = current.Id,
                Publication = publication,
                Created = DateTime.Now,
                Content = content
            };

            await _commentService.CreateAsync(com);

            return RedirectToAction("Index");
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
