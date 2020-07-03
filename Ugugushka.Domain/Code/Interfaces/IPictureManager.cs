using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Code.Interfaces
{
    public interface IPictureManager
    {
        Cloudinary Cloudinary { get; }
        Task<ToyImageDto> UploadPictureAsync(IFormFile formFile);
        Task<IEnumerable<ToyImageDto>> UploadPicturesAsync(IEnumerable<IFormFile> formFiles);
        Task DeleteTemporaryPicturesAsync();
        Task DeletePictureAsync(List<string> publicIds);
        Task ChangePictureTagAsync(List<string> publicIds, string newTag);
    }
}
