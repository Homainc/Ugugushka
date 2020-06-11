using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Abstractions;

namespace Ugugushka.WebUI.ViewModels
{
    public class AddToyViewModel : AbstractCloudinaryModel
    {
        public AddToyViewModel() : base(null) { }
        public AddToyViewModel(Cloudinary cloudinary) : base(cloudinary) { }

        public int Id { get; set; }

        [DisplayName("Название")]
        [MaxLength(70, ErrorMessage = "Количество символов названия не должно превышать 70!")]
        [Required(ErrorMessage = "Вы должны ввести название!")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        [MaxLength(500, ErrorMessage = "Количество символов описания не должно превышать 500!")]
        [Required(ErrorMessage = "Вы должны ввести описание")]
        public string Description { get; set; }

        [DisplayName("Категория")] public uint? CategoryId { get; set; }

        [DisplayName("Цена")]
        [Required(ErrorMessage = "Вы должны ввести цену!")]
        [DataType(DataType.Currency, ErrorMessage = "Некорректное значение")]
        [Range(0, Double.MaxValue, ErrorMessage = "Цена должна быть больше 0!")]
        public decimal Price { get; set; }

        [MinLength(1, ErrorMessage = "Нужно добавить как минимум 1 фотографию!")]
        [MaxLength(5, ErrorMessage = "Максимально возможное количество фотографий - 5!")]
        public List<ToyImageDto> Images { get; set; } = new List<ToyImageDto>();
    }
}
