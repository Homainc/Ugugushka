using System.ComponentModel.DataAnnotations;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.WebUI.ViewModels
{
    public class ToyInfoViewModel
    {
        public ToyDto Toy { get; set; }

        [Display(Name = "Количество")] 
        public int Quantity { get; set; } = 1;
    }
}
