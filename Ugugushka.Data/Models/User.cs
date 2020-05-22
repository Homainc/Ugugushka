using Microsoft.AspNetCore.Identity;

namespace Ugugushka.Data.Models
{
    public class User : IdentityUser
    {
        public string AvatarUrl { get; set; }
    }
}
