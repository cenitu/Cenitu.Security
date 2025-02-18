using Cenitu.Security.Dtos;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor;
using System.Net.Http.Json;
using System.Text.Json;

namespace Cenitu.Security.BlazorWebAssembly.Adaptors
{
    public class PagedResponse<T>
    {
        public List<T> Result { get; set; } = new();
        public int Count { get; set; }
    }

    // Özel adaptor
    public class AuthWebApiAdaptor : DataAdaptor
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _endPoint;

        // baseUrl => "https://localhost:5001/api/Products/paged" gibi tam endpoint.
        public AuthWebApiAdaptor(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
          
        }

        // Grid, veri okumak istediğinde bu metodu çağırır
        public override async Task<object> ReadAsync(DataManagerRequest dm, string key = null)
        {
            // 1) Named HttpClient => "Auth"
            var httpClient = _clientFactory.CreateClient("Auth");
            

            // 2) "dm" içinden skip/take/sort/filter bilgileri alınabilir
            int skip = dm.Skip;
            int take = dm.Take;

            // Sıralama veya filtreleme parametrelerini URL'ye eklemek isterseniz
            // dm.Sorted, dm.Search, dm.Where vb. inceleyip queryString oluşturabilirsiniz.

            // 3) Endpoint => "?skip=dm.Skip&take=dm.Take"
            var url = $"{httpClient.BaseAddress!.ToString()+_endPoint}?skip={skip}&take={take}";

            // 4) İstek at
            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                // Hata yönetimi: Boş veri dönebilirsiniz veya exception fırlatabilirsiniz
                return new { result = new List<object>(), count = 0 };
            }

            // 5) JSON parse
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                PropertyNameCaseInsensitive = true
            };
            // ProductListDto -> kendi modeliniz
            var pagedResponse = await response.Content.ReadFromJsonAsync<PagedResponse<ProductListDto>>(options);

            // 6) Syncfusion, server-side data için
            //    { result = <liste>, count = <toplam> } formatını bekliyor
            return new
            {
                result = pagedResponse?.Result ?? new List<ProductListDto>(),
                count = pagedResponse?.Count ?? 0
            };
        }

        // Eğer Create/Update/Delete kullanacaksanız:
        // public override async Task<object> InsertAsync(DataManager dataManager, object value, string key) { ... }
        // public override async Task<object> UpdateAsync(DataManager dataManager, object value, string keyField, string key) { ... }
        // public override async Task<object> DeleteAsync(DataManager dataManager, object keyField, object value) { ... }
    }
}
