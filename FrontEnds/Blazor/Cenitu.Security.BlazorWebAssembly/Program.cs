using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Cenitu.Security.BlazorWebAssembly;
using Microsoft.AspNetCore.Components.Authorization;
using Cenitu.Security.BlazorWebAssembly.Services;
using Blazored.LocalStorage;
using Cenitu.Security.BlazorWebAssembly.Extensions;
using Syncfusion.Blazor;
using Cenitu.Security.BlazorWebAssembly.Adaptors;


Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWX1feHRURWRcWE1/X0E=");
var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.Services.AddSyncfusionBlazor();

builder.Services.AddAuthorizationCore();


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.AddTokenAuthentication();

builder.Services.AddScoped<AuthWebApiAdaptor>();
builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7292") });
builder.Services.AddSyncfusionBlazor();






await builder.Build().RunAsync();
