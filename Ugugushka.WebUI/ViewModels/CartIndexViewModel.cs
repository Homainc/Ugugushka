using CloudinaryDotNet;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Abstractions;

namespace Ugugushka.WebUI.ViewModels
{
    public class CartIndexViewModel : AbstractCloudinaryModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        public CartIndexViewModel(Cloudinary cloudinary) : base(cloudinary)
        {
        }
    }
}
