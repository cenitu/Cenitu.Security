using Cenitu.Security.Dtos;

namespace Cenitu.Security.BlazorWebAssembly.Managers
{
    public interface IProductManager
    {
        Task<ProductAddDto> AddProductAsync(ProductAddDto productAddDto);
        Task<List<ProductListDto>> GetProductsAsync();
    }
}