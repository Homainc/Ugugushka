using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Ugugushka.WebUI.ViewModels
{
    public class AddToyViewModel
    {
        public uint Id { get; set; }
        
        [DisplayName("Название")]
        [Required(ErrorMessage = "Вы должны ввести название")]
        public string Name { get; set; }
        
        [DisplayName("Описание")]
        [Required(ErrorMessage = "Вы должны ввести описание")]
        public string Description { get; set; }
        
        [DisplayName("Категория")]
        public uint? CategoryId { get; set; }

        [DisplayName("Цена")]
        [Required(ErrorMessage = "Вы должны ввести цену")]
        [Range(0, Double.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
        public decimal Price { get; set; }
        [DisplayName("Есть на складе?")]
        public bool IsOnStock { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
