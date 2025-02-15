using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos;
using Cenitu.Security.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<List<RoleModel>> GetRolesAsync()
        {
            var roleList = roleManager.Roles.Select(x => new RoleModel
            {
                Id = Guid.Parse(x.Id),
                Name = x.Name
            }).ToList();
            return roleList;
        }

        public async Task<List<string>> GetUserRoleAsync(string emailId)
        {
            var user = await userManager.FindByEmailAsync(emailId);

            var userRoles = await userManager.GetRolesAsync(user);
            return userRoles.ToList();
        }
        public async Task<List<string>> AddRolesAsync(string[] roles)
        {
            var roleList = new List<string>();
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role });
                    roleList.Add(role);
                }
            }
            return roleList;
        }

        public async Task<bool> AddUserRoleAsync(string emailId, string[] roles)
        {
            var user = await userManager.FindByEmailAsync(emailId);


            var roleList = await CheckRoleExixtsAsync(roles);
            if (user != null && roleList.Count == roles.Length)
            {
                var result = await userManager.AddToRolesAsync(user, roleList);
                return result.Succeeded;
            }
            return false;
        }

        private async Task<List<string>> CheckRoleExixtsAsync(string[] roles)
        {
            var roleList = new List<string>();
            foreach (var role in roles)
            {
                if (await roleManager.RoleExistsAsync(role))
                {
                    roleList.Add(role);
                }
            }
            return roleList;
        }

        public async Task<bool> SeedAdminUserAndRoleAsyc()
        {
            var roleCount = roleManager.Roles.Count();
            var userCount = userManager.Users.Count();
            var adminUser = new ApplicationUser { Email = "admin@cenitusecurity.com", UserName = "admin@cenitusecurity.com" };
            if (roleCount == 0 && userCount == 0)
            {
                var role = await roleManager.CreateAsync(new ApplicationRole { Name = "Admin" });
                var userCreated = await userManager.CreateAsync(adminUser, "Admin@123");
                var user = await userManager.FindByEmailAsync("admin@cenitusecurity.com");
                if (user != null)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
