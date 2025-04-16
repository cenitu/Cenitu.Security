
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net.Http.Headers;

namespace Cenitu.Security.Fluent.Services
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
            //var refreshToken = await localStorage.GetItemAsync<string>("refreshToken");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //request.Headers.Authorization= new AuthenticationHeaderValue("Bearer", refreshToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
