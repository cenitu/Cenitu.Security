using Blazored.LocalStorage;
using Cenitu.Security.BlazorWebAssembly.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Cenitu.Security.BlazorWebAssembly.Extensions
{
    public static class CenituAuthenticationScheme
    {
        public static void AddCookieAuthentication(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddTransient<CustomHttpHandler>(); //For cookie based authentication
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>(); //For cookie based authentication
            builder.Services.AddHttpClient("Auth", opt =>
               opt.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:7064")).AddHttpMessageHandler<CustomHttpHandler>();
        }

        public static void AddTokenAuthentication(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddTransient<CustomHttpHandlerForTokenAuth>(); //For token based authentication
            builder.Services.AddScoped<AuthenticationStateProvider, CustomTokenAuthenticationStateProvider>(); //For token based authentication
            builder.Services.AddHttpClient("Auth", opt =>
               opt.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:7064")).AddHttpMessageHandler<CustomHttpHandlerForTokenAuth>();
        }
    }
}
