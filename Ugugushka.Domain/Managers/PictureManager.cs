using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Ugugushka.Domain.Code.Config;
using Ugugushka.Domain.Code.Constants;
using Ugugushka.Domain.Code.Exceptions;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Managers
{
    public class PictureManager : IPictureManager
    {
        private const int FileMaxLength = 10485760;
        private const string SizeErrorMessage = "Размер изображения должен быть не более 10.5Mb!";
        private const string IncorrectImageErrorMessage = "Некорректное изображение!";

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
                throw new ImageUploadException(SizeErrorMessage, fImage.FileName);

            await using var fStream = fImage.OpenReadStream();
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

            throw new ImageUploadException(IncorrectImageErrorMessage, fImage.FileName);
        }

        public async Task<IEnumerable<ToyImageDto>> UploadPicturesAsync(IEnumerable<IFormFile> fImages)
        {
            var images = new List<ToyImageDto>();
            foreach (var fImage in fImages)
                images.Add(await UploadPictureAsync(fImage));

            return images;
        }

        public async Task DeleteTemporaryPicturesAsync()
        {
            var response = await Cloudinary.ListResourcesByTagAsync(
                CloudinaryTagDefaults.Temp, cancellationToken: _cancellationToken);

            if (response.Error == null)
            {
                var checkDate = DateTime.Now.AddDays(-1);
                var expiredPublicIds = response.Resources.Where(x => DateTime.Parse(x.CreatedAt) < checkDate).Select(x => x.PublicId).ToList();

                if (expiredPublicIds.Count > 0)
                    await Cloudinary.DeleteResourcesAsync(new DelResParams
                    {
                        PublicIds = expiredPublicIds
                    }, _cancellationToken);
            }
        }

        public async Task DeletePictureAsync(List<string> publicIds)
        {
            await Cloudinary.DeleteResourcesAsync(new DelResParams
            {
                PublicIds = publicIds
            }, _cancellationToken);
        }

        public async Task ChangePictureTagAsync(List<string> publicIds, string newTag)
        {
            await Cloudinary.TagAsync(new TagParams
            {
                Command = TagCommand.Replace,
                PublicIds = publicIds,
                ResourceType = ResourceType.Image,
                Tag = newTag
            }, _cancellationToken);
        }
    }
}
