using Cenitu.Security.Dtos;

namespace Cenitu.Security.BlazorWebAssembly.Managers
{
    public interface IProductManager
    {
        Task<ProductAddDto> AddProductAsync(ProductAddDto productAddDto);
        Task<List<ProductListDto>> GetProductsAsync();
        Task<PagedAndSortedResult<ProductListDto>> GetProductsAsync(int page, int pageSize, string sortColumn, string sortDirection);
    }
}