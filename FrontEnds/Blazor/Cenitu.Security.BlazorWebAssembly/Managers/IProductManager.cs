using Cenitu.Security.Dtos;
using Cenitu.Security.Dtos.Product;

namespace Cenitu.Security.BlazorWebAssembly.Managers
{
    public interface IProductManager
    {
        Task<ProductAddDto> AddProductAsync(ProductAddDto productAddDto);
        Task<ProductAddDto> AddProductAsync(ProductCreateDto productAddDto);
        Task<ProductListDto> GetProductAsync(string Id);
        Task<List<ProductListDto>> GetProductsAsync();
        Task<PagedAndSortedResult<ProductListDto>> GetProductsAsync(int page, int pageSize, string sortColumn, string sortDirection);
        Task<ProductListDto> UpdateProductAsync(ProductListDto productDto);
    }
}