using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ugugushka.Domain.Code.Interfaces;

namespace Ugugushka.WebUI.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> UploadImages(IFormFile image)
        {
            if (image == null)
                return BadRequest();

            var uploadedUrl = await _pictureManager.UploadPictureAsync(image);
            if(uploadedUrl != null)
                return Ok(new { SecureUrl = uploadedUrl });

            return BadRequest();
        }
    }
}
