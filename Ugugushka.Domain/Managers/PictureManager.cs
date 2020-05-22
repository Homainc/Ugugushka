using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Ugugushka.Domain.Code.Interfaces;

namespace Ugugushka.Domain.Managers
{
    public class CloudinaryCredentials
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string CloudName { get; set; }
    }

    public class PictureManager : IPictureManager
    {
        private readonly Cloudinary _cloudinary;
        private readonly CancellationToken _cancellationToken;
        public PictureManager(IOptions<CloudinaryCredentials> options, IHttpContextAccessor httpContextAccessor)
        { 
            var credentials = options.Value;
            var account = new Account
            {
                ApiKey = credentials.ApiKey,
                ApiSecret = credentials.ApiSecret,
                Cloud = credentials.CloudName
            };
            _cloudinary = new Cloudinary(account);
            _cancellationToken = httpContextAccessor.HttpContext.RequestAborted;
        }
        public async Task<string> UploadPictureAsync(IFormFile fImage)
        {
            await using (var fStream = fImage.OpenReadStream())
            {
                var result = await _cloudinary.UploadAsync(new ImageUploadParams
                {
                    File = new FileDescription("temp", fStream)
                }, _cancellationToken);

                if (result.Error == null)
                    return result.SecureUri.ToString();
            }

            return null;
        }

        public Task DeletePictureAsync(string url)
        {
            throw new System.NotImplementedException();
        }

        public Task DeletePictureAsync(IEnumerable<string> urls)
        {
            throw new System.NotImplementedException();
        }
    }
}
