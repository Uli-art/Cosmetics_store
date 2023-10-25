using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Diagnostics;
using WEB_153502_Sidorova.IdentityServer.Data.Migrations;
using WEB_153502_Sidorova.IdentityServer.Models;

namespace WEB_153502_Sidorova.IdentityServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AvatarController : ControllerBase
    {
        private IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;

        public AvatarController(IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _environment = environment;
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var id = _userManager.GetUserId(User);
            var FolderPath = Path.Combine(_environment.ContentRootPath, "Images");

            var avatarPath = Path.Combine(FolderPath, id) + ".jpg";

            if (System.IO.File.Exists(avatarPath))
            {
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(avatarPath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var stream = new FileStream(avatarPath, FileMode.Open, FileAccess.Read);
                return File(stream, contentType);
            }
            else if (System.IO.File.Exists(Path.Combine(FolderPath, id) + ".png")) 
            {
                avatarPath = Path.Combine(FolderPath, id) + ".png";
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(avatarPath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var stream = new FileStream(avatarPath, FileMode.Open, FileAccess.Read);
                return File(stream, contentType);
            }
            else
            {
                var placeholderPath = Path.Combine(FolderPath, "profile-picture.png");

                if (System.IO.File.Exists(placeholderPath))
                {
                    var provider = new FileExtensionContentTypeProvider();
                    if (!provider.TryGetContentType(placeholderPath, out var contentType))
                    {
                        contentType = "application/octet-stream";
                    }

                    var stream = new FileStream(placeholderPath, FileMode.Open, FileAccess.Read);
                    return File(stream, contentType);
                }
                else
                {
                    return NotFound("Image not found");
                }
            }
        }
    }
}
