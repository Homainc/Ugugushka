using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ugugushka.Common.Concretes;
using Ugugushka.WebUI.Code.Attributes;
using Ugugushka.WebUI.Code.Constants;

namespace Ugugushka.WebUI.ViewModels
{
    public class CartCheckoutViewModel
    {
        [Required(ErrorMessage = ValidationMessageDefaults.Required)]
        [MaxLength(256)]
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = ValidationMessageDefaults.Required)]
        [MaxLength(256)]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = ValidationMessageDefaults.Required)]
        [EmailAddress(ErrorMessage = ValidationMessageDefaults.Incorrect)]
        [MaxLength(256)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessageDefaults.Required)]
        [Phone(ErrorMessage = ValidationMessageDefaults.Incorrect)]
        [MaxLength(50)]
        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = ValidationMessageDefaults.Required)]
        [DisplayName("Способ доставки")]
        public DeliveryWay DeliveryType { get; set; }

        [RequiredIf(nameof(DeliveryType), DeliveryWay.Courier, ValidationMessageDefaults.Required)]
        [MaxLength(256)]
        [DisplayName("Улица")]
        public string Street { get; set; }

        [RequiredIf(nameof(DeliveryType), DeliveryWay.Courier, ValidationMessageDefaults.Required)]
        [MaxLength(50)]
        [DisplayName("Номер дома")]
        public string HouseNumber { get; set; }

        [RequiredIf(nameof(DeliveryType), DeliveryWay.Courier, ValidationMessageDefaults.Required)]
        [MaxLength(50)]
        [DisplayName("Номер квартиры")]
        public string ApartmentNumber { get; set; }

        [RequiredIf(nameof(DeliveryType), DeliveryWay.Courier, ValidationMessageDefaults.Required)]
        [DisplayName("Этаж")]
        public int? FloorNumber { get; set; }

        [RequiredIf(nameof(DeliveryType), DeliveryWay.Courier, ValidationMessageDefaults.Required)]
        [MaxLength(50)]
        [DisplayName("Номер подъезда")]
        public string ExitNumber { get; set; }
    }
}
