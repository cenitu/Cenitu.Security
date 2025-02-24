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
        Task<ProductUpdateDto> GetProductAsync(int Id);
        Task<ApiResponse<ProductListDto>> GetProductsAsync(int skip, int? top, string? filter, string? orderby);
        Task<ProductUpdateDto> UpdateProductAsync(ProductUpdateDto productDto);
    }
}
