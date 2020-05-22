using System.ComponentModel.DataAnnotations;

namespace Ugugushka.WebUI.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [Required] [Display(Name = "Пароль")] 
        public string Password { get; set; }

        [Display(Name = "Запомнить?")] 
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
