using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Ugugushka.Domain.Code.Constants;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.UnitTests.FakeConcretes
{
    public class FakePictureManager : IPictureManager
    {
        public ISet<string> PublicIdsWithToyTag { get; set; } = new HashSet<string>();
        public ISet<string> PublicIdsWithTempTag { get; set; } = new HashSet<string>();
        public Cloudinary Cloudinary { get; }

        public Task<ToyImageDto> UploadPictureAsync(IFormFile formFile) => null;
        public Task<IEnumerable<ToyImageDto>> UploadPicturesAsync(IEnumerable<IFormFile> formFiles) => null;

        public Task DeleteTemporaryPicturesAsync() => Task.CompletedTask;

        public Task DeletePictureAsync(List<string> publicIds) => Task.CompletedTask;

        public Task ChangePictureTagAsync(List<string> publicIds, string newTag)
        {
            switch (newTag)
            {
                case CloudinaryTagDefaults.Temp:
                    PublicIdsWithTempTag.UnionWith(publicIds);
                    PublicIdsWithToyTag.ExceptWith(publicIds);
                    break;
                case CloudinaryTagDefaults.Toy:
                    PublicIdsWithTempTag.ExceptWith(publicIds);
                    PublicIdsWithToyTag.UnionWith(publicIds);
                    break;;
            }
            return Task.CompletedTask;
        }
    }
}
