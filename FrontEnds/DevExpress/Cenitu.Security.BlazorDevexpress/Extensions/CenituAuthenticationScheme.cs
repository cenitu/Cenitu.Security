
using Cenitu.Security.BlazorDevExpress.Services;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Cenitu.Security.BlazorDevExpress.Extensions
{
    public static class CenituAuthenticationScheme
    {
        public static void AddCookieAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<CustomHttpHandler>(); //For cookie based authentication
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>(); //For cookie based authentication
            builder.Services.AddHttpClient("Auth", opt =>
               opt.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:7064")).AddHttpMessageHandler<CustomHttpHandler>();


            builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7700") });

        }

        
        //public static void AddTokenAuthentication(this WebAssemblyHostBuilder builder)
        //{
        //    builder.Services.AddBlazoredLocalStorage();
        //    builder.Services.AddTransient<CustomHttpHandlerForTokenAuth>(); //For token based authentication
        //    builder.Services.AddScoped<AuthenticationStateProvider, CustomTokenAuthenticationStateProvider>(); //For token based authentication
        //    builder.Services.AddHttpClient("Auth", opt =>
        //       opt.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:7064")).AddHttpMessageHandler<CustomHttpHandlerForTokenAuth>();
            
        //}
      


    }
}
