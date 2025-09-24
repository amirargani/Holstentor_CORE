using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holstentor.Data.Class_DbContextManageUser
{
    public class Initializer
    {
        public static async Task Initial(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admmin"))
            {
                var users = new IdentityRole("Admin");
                await roleManager.CreateAsync(users);
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                var users = new IdentityRole("User");
                await roleManager.CreateAsync(users);
            }
            if (!await roleManager.RoleExistsAsync("Manager"))
            {
                var users = new IdentityRole("Manager");
                await roleManager.CreateAsync(users);
            }
        }
    }
}