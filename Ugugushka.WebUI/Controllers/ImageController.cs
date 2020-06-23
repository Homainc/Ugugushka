using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ugugushka.Domain.Code.Constants;
using Ugugushka.Domain.Code.Interfaces;

namespace Ugugushka.WebUI.Controllers
{
    [Authorize(Roles = RoleDefaults.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IPictureManager _pictureManager;
        public ImageController(IPictureManager pictureManager)
        {
            _pictureManager = pictureManager;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImages(IFormFileCollection images)
        {
            if (images == null)
                return BadRequest();

            return Ok(await _pictureManager.UploadPicturesAsync(images));
        }
    }
}
