
using Ajay.PMS.Models;
using Microsoft.AspNetCore.Identity;

namespace IMS.web.Data
{
    public class SeedingData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<UserLogin>>();

            string[] Roles = { "ADMIN", "CUSTOMER" };

            foreach (string roleName in Roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }


            if (await _userManager.FindByNameAsync("admin@gmail.com") == null)
            {
                var role = _roleManager.FindByNameAsync("ADMIN").Result;

                var user = new UserLogin()
                {

                    FirstName = "Admin",
                    MiddleName = "",
                    LastName = "",                 
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    PhoneNumber = "9823211878",
                    Address = "Bhaktapur",
                    CreatedDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, "Admin@123");
                await _userManager.SetLockoutEnabledAsync(user, false);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "ADMIN");
                }
            }
        }
    }
}
