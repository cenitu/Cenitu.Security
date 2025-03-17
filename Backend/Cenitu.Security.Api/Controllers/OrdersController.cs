using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos.Order;
using Cenitu.Security.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Cenitu.Security.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IOrderService orderService;
        public OrdersController(AppDbContext appDbContext, IOrderService orderService)
        {
            _appDbContext = appDbContext;
            this.orderService = orderService;
        }

        [HttpGet]

        public async Task<IActionResult> Get([FromQuery(Name = "$inlinecount")] string? inlinecount,
            [FromQuery(Name = "$skip")] int skip,
            [FromQuery(Name = "$top")] int? top,
            [FromQuery(Name = "$filter")] string? filter,
            [FromQuery(Name = "$orderby")] string? orderby)
        {
            var result =await orderService.GetOrdersAsync(skip, top, filter, orderby);
            if (inlinecount == null)
            {
                return Ok(result.Items);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductionOrderCreateDto order)
        {

            await orderService.AddOrderAsync(order);
            return Ok(order);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await orderService.DeleteOrderAsync(id);
            return Ok();
        }
    }
}
