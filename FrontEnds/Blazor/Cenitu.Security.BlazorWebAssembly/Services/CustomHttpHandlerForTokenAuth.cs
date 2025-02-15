
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net.Http.Headers;

namespace Cenitu.Security.BlazorWebAssembly.Services
{
    public class CustomHttpHandlerForTokenAuth:DelegatingHandler
    {
        private readonly ILocalStorageService localStorage;

        public CustomHttpHandlerForTokenAuth(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            var accessToken = await localStorage.GetItemAsync<string>("accessToken");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
