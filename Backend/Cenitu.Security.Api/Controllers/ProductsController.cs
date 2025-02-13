using Cenitu.Security.Dtos;
using Cenitu.Security.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cenitu.Security.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        [Authorize(Roles = "Admin, User")]  
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var productList = await productService.GetProductsAsync();
            return Ok(productList);
        }
        [Authorize(Roles= "Admin")]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductAddDto productAddDto)
        {
            var result = await productService.AddProductAsync(productAddDto);
            return Ok(result);
        }
    }
}
