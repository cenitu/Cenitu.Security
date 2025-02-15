using Cenitu.Security.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cenitu.Security.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly IRoleService roleService;

        public SeedController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpPost("SeedAdminUserAndRole")]
        public async Task<IActionResult> SeedAdminUserAndRole()
        {
            var result = await roleService.SeedAdminUserAndRoleAsyc();
            if (!result)
            {
                return BadRequest("Admin user already created");
            }
            return Ok("Admin user and role successfully created");
        }
    }
}
