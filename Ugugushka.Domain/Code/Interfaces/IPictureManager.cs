using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ugugushka.Domain.Code.Interfaces
{
    public interface IPictureManager
    {
        Task<string> UploadPictureAsync(IFormFile formFile);
        Task DeletePictureAsync(string url);
        Task DeletePictureAsync(IEnumerable<string> urls);
    }
}
