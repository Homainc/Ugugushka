using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ugugushka.WebUI.Code.Constants;

namespace Ugugushka.WebUI.ViewModels
{
    public class AdminAddPartitionViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        [Required(ErrorMessage = ValidationMessageDefaults.Required)]
        [MaxLength(70, ErrorMessage = "Максимальная длина - {0}")]
        public string Name { get; set; }
    }
}
