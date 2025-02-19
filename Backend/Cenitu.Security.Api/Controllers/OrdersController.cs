using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Cenitu.Security.Api.Controllers
{
    //[Route("api/odata/[controller]")]
    public class OrdersController : ODataController
    {
        private readonly AppDbContext _appDbContext;
        public OrdersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_appDbContext.Orders);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            _appDbContext.Orders.Add(order);
            await _appDbContext.SaveChangesAsync();
            return Ok(order);
        }
    }
}
