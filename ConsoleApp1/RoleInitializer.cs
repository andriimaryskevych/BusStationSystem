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
            string directorName = "director";
            string password = "director131";
            if (await roleManager.FindByNameAsync(Roles.director) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.director));
            }
            if (await roleManager.FindByNameAsync(Roles.employee) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.employee));
            }
            if (await userManager.FindByNameAsync(directorName) == null)
            {
                var admin = new User { UserName = directorName };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Roles.director);
                }
            }
        }
    }
}
