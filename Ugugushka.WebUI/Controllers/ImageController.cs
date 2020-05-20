using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ugugushka.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> UploadImages(IFormFile image)
        {
            if (image == null)
                return BadRequest();

            var account = new Account
            {
                ApiKey = "791753898357469",
                ApiSecret = "yM5iIqxRBxJHcyJ7X3L_GNWKHlQ",
                Cloud = "dofujaj9p"
            };


            var cl = new Cloudinary(account);
            await using (var fStream = image.OpenReadStream())
            {
                var result = await cl.UploadAsync(new ImageUploadParams
                {
                    File = new FileDescription("temp", fStream)
                });

                if (result.Error == null)
                {
                    return Ok(new { SecureUrl = result.SecureUri.ToString() });
                }
            }

            return BadRequest();
        }
    }
}
