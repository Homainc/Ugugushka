using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Abstractions;

namespace Ugugushka.WebUI.ViewModels
{
    public class HomeToyInfoViewModel : AbstractCloudinaryModel
    {
        public HomeToyInfoViewModel(ToyDto toy, Cloudinary cloudinary) : base(cloudinary) => 
            Toy = toy;
        
        [ValidateNever]
        public ToyDto Toy { get; set; }

        [ValidateNever]
        public IEnumerable<ToyDto> SimilarToys { get; set; } = new List<ToyDto>();

        [Display(Name = "Количество")] 
        public int Quantity { get; set; } = 1;
    }
}
