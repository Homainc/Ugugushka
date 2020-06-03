using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Ugugushka.Data.Models;
using Ugugushka.Domain.Code.Config;
using Ugugushka.Domain.Code.Constants;
using Ugugushka.Domain.Code.Interfaces;

namespace Ugugushka.Domain.Managers
{
    public class IdentitySeedManager : ISeedManager
    {
        private readonly AdminAccountCredentials _adminCredentials;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentitySeedManager(IOptions<CredentialsConfig> options, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _adminCredentials = options.Value.AdminAccount;
        }

        public void SeedData()
        {
            SeedRoles(_roleManager);
            SeedUsers(_userManager);
        }

        private void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByEmailAsync(_adminCredentials.Email).Result == null)
            {
                var user = new User
                {
                    Email = _adminCredentials.Email,
                    EmailConfirmed = true,
                    UserName = _adminCredentials.Username
                };

                var result = userManager.CreateAsync(user, _adminCredentials.Password).Result;
                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, RoleDefaults.Admin);
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(RoleDefaults.User).Result)
            {
                var role = new IdentityRole(RoleDefaults.User);
                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync(RoleDefaults.Admin).Result)
            {
                var role = new IdentityRole(RoleDefaults.Admin);
                roleManager.CreateAsync(role).Wait();
            }
        }
    }
}
