using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace NBD6.Data
{
    public class ApplicationDbInitializer
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            ApplicationDbContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<ApplicationDbContext>();
            try
            {
                //Create the database if it does not exist and apply the Migration
                context.Database.Migrate();

                //Create Roles
                var RoleManager = applicationBuilder.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roleNames = { "Admin", "Management", "Designer", "Sales" };
                IdentityResult roleResult;
                foreach (var roleName in roleNames)
                {
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
                //Create Users
                var userManager = applicationBuilder.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                if (userManager.FindByEmailAsync("admin@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "admin@outlook.com",
                        Email = "admin@outlook.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "admin").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }
                if (userManager.FindByEmailAsync("management@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "management@outlook.com",
                        Email = "management@outlook.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "management").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Management").Wait();
                    }
                }
                if (userManager.FindByEmailAsync("designer@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "designer@outlook.com",
                        Email = "designer@outlook.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "designer").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Designer").Wait();
                    }
                }
                if (userManager.FindByEmailAsync("sales@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "sales@outlook.com",
                        Email = "sales@outlook.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "sales").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Sales").Wait();
                    }
                }
                if (userManager.FindByEmailAsync("user@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "user@outlook.com",
                        Email = "user@outlook.com"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "user").Result;
                    //Not in any role
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
