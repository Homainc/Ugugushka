using System.ComponentModel;

namespace Ugugushka.WebUI.ViewModels
{
    public class CartCheckoutViewModel
    {
        [DisplayName("Ваше имя")]
        public string FirstName { get; set; }
        [DisplayName("Ваша фамилия")]
        public string LastName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Ваш номер телефона")]
        public string PhoneNumber { get; set; }
        [DisplayName("Улица")]
        public string Street { get; set; }
        [DisplayName("Номер дома")]
        public string HouseNumber { get; set; }
        [DisplayName("Номер квартиры")]
        public string ApartmentNumber { get; set; }
        [DisplayName("Этаж")]
        public int FloorNumber { get; set; }
        [DisplayName("Номер подъезда")]
        public string ExitNumber { get; set; }
    }
}
