using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos;
using Cenitu.Security.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductCreateDto> AddProductAsync(ProductCreateDto productCreateDto);
        IQueryable<Product> Get();
        Task<ProductListDto> GetProductAsync(int Id);
        Task<ApiResponse<ProductListDto>> GetProducts(int skip, int top, string? filter, string? orderby);
        Task<ProductListDto> UpdateProductAsync(ProductListDto productDto);
    }
}
