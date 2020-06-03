using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Ugugushka.Domain.Code.Config;
using Ugugushka.Domain.Code.Constants;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Managers
{
    public class PictureManager : IPictureManager
    {
        private const int FileMaxLength = 10485760;

        private readonly CancellationToken _cancellationToken;

        public Cloudinary Cloudinary { get; }

        public PictureManager(IOptions<CredentialsConfig> options, IHttpContextAccessor httpContextAccessor)
        {
            var credentials = options.Value;
            var account = new Account
            {
                ApiKey = credentials.Cloudinary.ApiKey,
                ApiSecret = credentials.Cloudinary.ApiSecret,
                Cloud = credentials.Cloudinary.CloudName
            };
            Cloudinary = new Cloudinary(account);
            _cancellationToken = httpContextAccessor.HttpContext.RequestAborted;
        }

        public async Task<ToyImageDto> UploadPictureAsync(IFormFile fImage)
        {
            if (fImage.Length > FileMaxLength)
                return null;

            await using (var fStream = fImage.OpenReadStream())
            {
                var result = await Cloudinary.UploadAsync(new ImageUploadParams
                {
                    File = new FileDescription(CloudinaryTagDefaults.Temp, fStream),
                    Tags = CloudinaryTagDefaults.Temp
                }, _cancellationToken);

                if (result.Error == null)
                {
                    return new ToyImageDto
                    {
                        PublicId = result.PublicId,
                        Format = result.Format
                    };
                }
            }

            return null;
        }

        public async Task DeleteTemporaryPicturesAsync() =>
            await Cloudinary.DeleteResourcesByTagAsync(CloudinaryTagDefaults.Temp, _cancellationToken);

        public async Task DeletePictureAsync(List<string> publicIds)
        {
            await Cloudinary.DeleteResourcesAsync(new DelResParams
            {
                PublicIds = publicIds
            });
        }

        public async Task ChangePictureTagAsync(List<string> publicIds, string newTag)
        {
            await Cloudinary.TagAsync(new TagParams
            {
                Command = TagCommand.Replace,
                PublicIds = publicIds,
                ResourceType = ResourceType.Image,
                Tag = newTag
            });
        }
    }
}
