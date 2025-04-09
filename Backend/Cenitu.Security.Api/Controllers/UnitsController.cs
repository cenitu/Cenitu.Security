using Cenitu.Security.Dtos.Product;
using Cenitu.Security.Dtos.Unit;
using Cenitu.Security.Services.Interfaces;
using Cenitu.Security.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cenitu.Security.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitService unitService;
        public UnitsController(IUnitService unitService)
        {
            this.unitService = unitService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUnits([FromQuery(Name = "$inlinecount")] string? inlinecount,
            [FromQuery(Name = "$skip")] int skip,
            [FromQuery(Name = "$top")] int? top,
            [FromQuery(Name = "$filter")] string? filter,
            [FromQuery(Name = "$orderby")] string? orderby)
        {
            var units = await unitService.GetUnitsAsync(skip, top, filter, orderby);
            if (inlinecount == null)
            {
                return Ok(units.Items);
            }
            return Ok(units);
        }
        [HttpGet("{Id}")]
        public async Task<UnitDto> GetProduct(int Id)
        {
            var result = await unitService.GetUnitAsync(Id);
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateUnitAsync([FromBody] UnitDto unitDto)
        {
           
            var result = await unitService.UpdateUnitAsync(unitDto);
            return Ok(result);
        }

    }
}
