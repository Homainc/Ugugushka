using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Abstractions;

namespace Ugugushka.WebUI.ViewModels
{
    public class ToyInfoViewModel : AbstractCloudinaryModel
    {
        public ToyInfoViewModel(Cloudinary cloudinary) : base(cloudinary) { }

        public ToyDto Toy { get; set; }

        [Display(Name = "Количество")] 
        public int Quantity { get; set; } = 1;
    }
}
