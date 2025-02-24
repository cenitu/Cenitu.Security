using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos;
using Cenitu.Security.Dtos.Product;
using Cenitu.Security.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Cenitu.Security.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProductsAsync(
            [FromQuery(Name = "$inlinecount")] string inlinecount,
            [FromQuery(Name = "$skip")] int skip,
            [FromQuery(Name = "$top")] int top,
            [FromQuery(Name = "$filter")] string? filter,
            [FromQuery(Name = "$orderby")] string? orderby)
        {
            var productList = await _productService.GetProductsAsync(skip, top, filter, orderby);
            return Ok(productList);
        }
        //[Authorize(Roles = "Admin, User")]
        //[HttpGet("GetProducts")]
        //public async Task<IActionResult> GetProducts()
        //{
        //    var productList = await productService.GetProductsAsync();
        //    return Ok(productList);
        //}
        //[Authorize(Roles = "Admin")]
        //[HttpPost("AddProduct")]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto productAddDto)
        {
            var result = await _productService.AddProductAsync(productAddDto);
            return Ok(result);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProductAsync([FromBody] ProductUpdateDto productUpdateDto)
        {
            var result = await _productService.UpdateProductAsync(productUpdateDto);
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("GetProduct")]
        public async Task<ProductListDto> GetProduct(int Id)
        {
            var result = await _productService.GetProductAsync(Id);
            return result;
        }
        
  
    }
}
