using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Constants;

namespace Ugugushka.WebUI.ViewModels
{
    public class AdminAddCategoryViewModel
    {
        public int Id { get; set; }
        
        [DisplayName("Название")]
        [Required(ErrorMessage = ValidationMessageDefaults.Required)]
        [MaxLength(70)]
        public string Name { get; set; }

        [DisplayName("Раздел")]
        [Required(ErrorMessage = ValidationMessageDefaults.Required)]
        public int PartitionId { get; set; }
    }
}
