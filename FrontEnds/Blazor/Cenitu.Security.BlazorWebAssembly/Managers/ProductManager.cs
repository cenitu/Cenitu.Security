using Cenitu.Security.Dtos;
using System.Net.Http.Json;

namespace Cenitu.Security.BlazorWebAssembly.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory httpClientFactory;

        public ProductManager(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("Auth");
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<ProductAddDto> AddProductAsync(ProductAddDto productAddDto)
        {
            var response = await httpClient.PostAsJsonAsync("api/Products/AddProduct", productAddDto);
            return await response.Content.ReadFromJsonAsync<ProductAddDto>() ?? new ProductAddDto();
        }
        public async Task<List<ProductListDto>> GetProductsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<ProductListDto>>("api/Products/GetProducts") ?? new List<ProductListDto>();
        }
    }
}
