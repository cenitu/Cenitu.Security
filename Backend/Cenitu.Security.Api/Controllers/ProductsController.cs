using Cenitu.Security.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Cenitu.Security.Api.Controllers
{
    [Route("api/odata/[controller]")]
    public class ProductsController : ODataController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
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
        //public async Task<IActionResult> AddProduct([FromBody] ProductAddDto productAddDto)
        //{
        //    var result = await productService.AddProductAsync(productAddDto);
        //    return Ok(result);
        //}
        //[Authorize(Roles = "Admin, User")]
        //[HttpGet("GetProductsPaged")]
        //public async Task<IActionResult> GetProductsPaged(int page = 1, int pageSize = 10, string sortColumn = "Id", string sortDirection = "asc")
        //{
        //    var products = await productService.GetProductsPaged(page, pageSize, sortColumn, sortDirection);
        //    var jsonProducts = JsonSerializer.Serialize(products);  
        //    return Ok(products  );
        //}
        //[Authorize(Roles = "Admin, User")]
        //[HttpGet("GetPagedData")]

        //public async Task<IActionResult> GetPagedData([FromQuery] int skip = 0, [FromQuery] int take = 10)
        //{
        //    // totalCount
        //    var result = await productService.GetProductsPaged(skip, take);
        //    var list = result.Data;
        //    var totalCount = result.TotalCount;


        //    // Syncfusion'a uygun json => { result, count }
        //    return Ok(new
        //    {
        //        result = list,
        //        count = totalCount,
        //    });
        //}
        [Authorize(Roles = "Admin, User")]
        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            var products = _productService.Get();
            
            return Ok(products);
        }
    }
}
