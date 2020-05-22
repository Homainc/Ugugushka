using Microsoft.AspNetCore.Identity;
using Ugugushka.Data.Models;
using Ugugushka.Domain.Code.Constants;
using Ugugushka.Domain.Code.Extensions;
using Ugugushka.Domain.Code.Interfaces;

namespace Ugugushka.Domain.Managers
{
    public class IdentitySeedManager : ISeedManager
    {
        private const string AdminEmail = "spritefok@gmail.com";
        private const string AdminPassword = "123456@aA";

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentitySeedManager(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedData()
        {
            SeedRoles(_roleManager);
            SeedUsers(_userManager);
        }

        private static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByEmailAsync(AdminEmail).Result == null)
            {
                var user = new User
                {
                    Email = AdminEmail,
                    EmailConfirmed = true,
                    UserName = AdminEmail.ToUserName()
                };

                var result = userManager.CreateAsync(user, AdminPassword).Result;
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
