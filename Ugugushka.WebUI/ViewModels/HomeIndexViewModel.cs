using CloudinaryDotNet;
using Ugugushka.Common.Interfaces;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Abstractions;

namespace Ugugushka.WebUI.ViewModels
{
    public class HomeIndexViewModel : AbstractCloudinaryModel
    {
        public IPagedResult<ToyDto> PagedToys { get; set; }
        public HomeIndexViewModel(Cloudinary cloudinary) : base(cloudinary)
        {
            
        }
    }
}
