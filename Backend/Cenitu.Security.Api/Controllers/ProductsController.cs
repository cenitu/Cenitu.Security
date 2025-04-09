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
      
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync(
            [FromQuery(Name = "$inlinecount")] string? inlinecount,
            [FromQuery(Name = "$skip")] int skip,
            [FromQuery(Name = "$top")] int? top,
            [FromQuery(Name = "$filter")] string? filter,
            [FromQuery(Name = "$orderby")] string? orderby)
        {
            var productList = await _productService.GetProductsAsync(skip, top, filter, orderby);
            if (inlinecount==null)
            {
                return Ok(productList.Items);
            }
            return Ok(productList);
        }
      
        [Authorize(Roles = "Admin")]
    
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto productCreateDto)
        {
        
            productCreateDto.CreatedByUserName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await _productService.AddProductAsync(productCreateDto);
            return Ok(result);
        }

    
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync([FromBody] ProductUpdateDto productUpdateDto)
        {
            productUpdateDto.LastModifiedByUserName = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await _productService.UpdateProductAsync(productUpdateDto);
            return Ok(result);
        }
        [Authorize(Roles = "Admin, User")]

        [HttpGet("{Id}")]
        public async Task<ProductUpdateDto> GetProduct(int Id)
        {
            var result = await _productService.GetProductAsync(Id);
            return result;
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
           
            return Ok();
        }

        [HttpGet("GetStocks")]
        public async Task<IActionResult> GetStocksAsync(
            [FromQuery(Name = "$inlinecount")] string? inlinecount,
            [FromQuery(Name = "$skip")] int skip,
            [FromQuery(Name = "$top")] int? top,
            [FromQuery(Name = "$filter")] string? filter,
            [FromQuery(Name = "$orderby")] string? orderby)
        {
            var stockList = await _productService.GetStocksAsync(skip, top, filter, orderby);
            if (inlinecount == null)
            {
                return Ok(stockList.Items);
            }
            return Ok(stockList);
        }

        [HttpGet("UpdateStocks")]
        public async Task<IActionResult> UpdateStocksAsync()
        {
            await _productService.UpdateProductStocksAsync();
            return Ok();
        }

    }
}
