using Cenitu.Security.Dtos;
using Cenitu.Security.Services.Interfaces;
using Cenitu.Security.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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
        [Authorize(Roles = "Admin")]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductAddDto productAddDto)
        {
            var result = await productService.AddProductAsync(productAddDto);
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetProductsPaged")]
        public async Task<IActionResult> GetProductsPaged(int page = 1, int pageSize = 10, string sortColumn = "Id", string sortDirection = "asc")
        {
            var products = await productService.GetProductsPaged(page, pageSize, sortColumn, sortDirection);
            var jsonProducts = JsonSerializer.Serialize(products);  
            return Ok(jsonProducts);
        }
    }
}
