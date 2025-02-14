﻿
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Cenitu.Security.BlazorWebAssembly.Services
{
    public class CustomHttpHandler:DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
