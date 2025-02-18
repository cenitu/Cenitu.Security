using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductAddDto> AddProductAsync(ProductAddDto productAddDto);
        IQueryable<Product> Get();
        Task<List<ProductListDto>> GetProductsAsync();
        Task<PagedAndSortedResult<ProductListDto>> GetProductsPaged(int page = 1, int pageSize = 10, string sortColumn = "Id", string sortDirection = "asc");
    }
}
