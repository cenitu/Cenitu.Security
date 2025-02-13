using Cenitu.Security.Dtos;
using Cenitu.Security.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cenitu.Security.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetUserRole")]
        public async Task<IActionResult> GetUserRole(string emailId)
        {
            var userRoles = await roleService.GetUserRoleAsync(emailId);
            return Ok(userRoles);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roleList = await roleService.GetRolesAsync();
            return Ok(roleList);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddRoles")]
        public async Task<IActionResult> AddRoles(string[] roles)
        {
            var roleList = await roleService.AddRolesAsync(roles);
            if (roleList.Count == 0)
            {
                return BadRequest("Roles already exists");
            }
            return Ok(roleList);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddUserRole")]
        public async Task<IActionResult> AddUserRole([FromBody] AddUserModel addUserModel)
        {
            var result = await roleService.AddUserRoleAsync(addUserModel.UserEmail, addUserModel.Roles);
            if (!result)
            {
                return BadRequest("User not found or Roles are not defined");
            }
            return Ok(result);
        }
    }
}
