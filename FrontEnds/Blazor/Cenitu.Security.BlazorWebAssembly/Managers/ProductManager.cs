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

        public async Task<PagedAndSortedList<ProductListDto>> GetProductsAsync(int page, int pageSize, string sortColumn, string sortDirection)
        {
            var response = await httpClient.GetFromJsonAsync<PagedAndSortedList<ProductListDto>>(
                $"api/Products/GetProductsPaged?page={page}&pageSize={pageSize}&sortColumn={sortColumn}&sortDirection={sortDirection}");

            return response ?? new PagedAndSortedList<ProductListDto>
            {
                Data = new List<ProductListDto>(),
                TotalCount = 0,
                PageSize = pageSize,
                PageIndex = page
            };
        }
    }
}
