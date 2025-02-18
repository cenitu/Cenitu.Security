using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Cenitu.Security.BlazorWebAssembly;
using Microsoft.AspNetCore.Components.Authorization;
using Cenitu.Security.BlazorWebAssembly.Services;
using Blazored.LocalStorage;
using Cenitu.Security.BlazorWebAssembly.Extensions;
using Syncfusion.Blazor;


Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWX1feHRURWRcWE1/X0E=");
var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.Services.AddSyncfusionBlazor();

builder.Services.AddAuthorizationCore();


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//// The code below enables the use of Blazored.LocalStorage. This is used to store the token in the browser's local storage. For cookie based authentication, this is not needed. You can comment out the following line.
//builder.Services.AddBlazoredLocalStorage();


//// The following code is for cookie based authentication. If you are using token based authentication, you can comment out the following two lines.
////builder.Services.AddTransient<CustomHttpHandler>(); //For cookie based authentication
////builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>(); //For cookie based authentication
////builder.Services.AddHttpClient("Auth", opt =>
////   opt.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:7064")).AddHttpMessageHandler<CustomHttpHandler>();

////End of cookie based authentication code   

//// The following code is for token based authentication. If you are using cookie based authentication, you can comment out the following two lines.
//builder.Services.AddTransient<CustomHttpHandlerForTokenAuth>(); //For token based authentication
//builder.Services.AddScoped<AuthenticationStateProvider, CustomTokenAuthenticationStateProvider>(); //For token based authentication
//builder.Services.AddHttpClient("Auth", opt =>
//   opt.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:7064")).AddHttpMessageHandler<CustomHttpHandlerForTokenAuth>();
//// End of token based authentication code

builder.AddTokenAuthentication();


builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7292") });
builder.Services.AddSyncfusionBlazor();






await builder.Build().RunAsync();
