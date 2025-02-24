using Cenitu.Security.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cenitu.Security.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductssController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductssController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery(Name = "$inlinecount")] string inlinecount,
            [FromQuery(Name = "$skip")] int skip,
            [FromQuery(Name = "$top")] int top,
            [FromQuery(Name ="$filter")] string? filter,
            [FromQuery(Name ="$orderby")] string? orderby)
        {
            var productList =await  _productService.GetProductsAsync(skip, top, filter,orderby);
            return Ok(productList);
        }
    }
}
