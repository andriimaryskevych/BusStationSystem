using BusStationSystem.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusStationSystem.BLL.Constants;
using System.Diagnostics.CodeAnalysis;

namespace BusStationSystem.BLL.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string owner = "owner";
            string password = "12121212";

            if (await roleManager.FindByNameAsync(Roles.director) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.director));
            }
            if (await roleManager.FindByNameAsync(Roles.employee) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.employee));
            }
            if (await roleManager.FindByNameAsync(Roles.owner) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.owner));
            }

            if (await userManager.FindByNameAsync(owner) == null)
            {
                var admin = new User { UserName = owner };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Roles.owner);
                }
            }
        }
    }
}
