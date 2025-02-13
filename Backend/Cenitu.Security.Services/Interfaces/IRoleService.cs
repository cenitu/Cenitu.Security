using Cenitu.Security.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleModel>> GetRolesAsync();
        Task<List<string>> GetUserRoleAsync(string emailId);
        Task<List<string>> AddRolesAsync(string[] roles);
        Task<bool> AddUserRoleAsync(string emailId, string[] roles);
    }
}
