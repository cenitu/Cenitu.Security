using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Cenitu.Security.BlazorWebAssembly;
using Microsoft.AspNetCore.Components.Authorization;
using Cenitu.Security.BlazorWebAssembly.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomHttpHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7292") });

builder.Services.AddHttpClient("Auth", opt=>
   opt.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:7064")).AddHttpMessageHandler<CustomHttpHandler>();
await builder.Build().RunAsync();
