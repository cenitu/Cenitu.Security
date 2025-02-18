using Cenitu.Security.Dtos;
using Syncfusion.Blazor;
using System.Text.Json;

namespace Cenitu.Security.BlazorWebAssembly.Adaptors
{
    public class GenericAuthAdaptor<T>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GenericAuthAdaptor(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<T>> GetOrdersAsync(string apiUrl)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Auth");

                var response = await client.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"HTTP Error: {response.StatusCode}");
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<T>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
                return new List<T>();
            }
        }
    }




}
