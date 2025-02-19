using Cenitu.Security.BlazorWebAssembly;
using Cenitu.Security.BlazorWebAssembly.Extensions;
using Cenitu.Security.BlazorWebAssembly.Resources;
using Cenitu.Security.BlazorWebAssembly.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;


Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWX1feHRURWRcWE1/X0E=");
var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.Services.AddSyncfusionBlazor();

builder.Services.AddSyncfusionBlazor();
// Register the locale service to localize the  SyncfusionBlazor components.
builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));

builder.Services.AddAuthorizationCore();


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.AddCookieAuthentication();

builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7292") });
builder.Services.AddSyncfusionBlazor();






await builder.Build().RunAsync();
