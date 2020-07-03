using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Abstractions;

namespace Ugugushka.WebUI.ViewModels
{
    public class ToyInfoViewModel : AbstractCloudinaryModel
    {
        public ToyInfoViewModel(Cloudinary cloudinary) : base(cloudinary) { }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ToyImageDto MainImage { get; set; }
        public IEnumerable<ToyImageDto> ExtraImages { get; set; }

        [Display(Name = "Количество")] 
        public int Quantity { get; set; } = 1;
    }
}
