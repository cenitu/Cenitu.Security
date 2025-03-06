using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Services.AutoMapper;
using Cenitu.Security.Services.Interfaces;
using Cenitu.Security.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAuthorization(options => { });

builder.Services
    .AddIdentityApiEndpoints<ApplicationUser>(opts =>
    {
        opts.Password.RequireNonAlphanumeric = true;
        opts.User.RequireUniqueEmail = true;
    })
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>();



builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    options.SlidingExpiration = true;
    options.Cookie.Name = "Cenitu.Security";
    options.LoginPath = "/api/identity/login";
    options.LogoutPath = "/api/identity/logout";
    options.AccessDeniedPath = "/api/identity/access-denied";
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});



builder.Services.AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme).Configure(options =>
{
    options.BearerTokenExpiration = TimeSpan.FromSeconds(3600);
   
});




builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddAutoMapper(typeof(MyMapper).Assembly);


builder.Services.AddControllers();

builder.Services.AddSwaggerGen(opts =>
{
    opts.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    opts.OperationFilter<SecurityRequirementsOperationFilter>();


});


builder.Services.AddCors(opts =>
{
    opts.AddPolicy("wasm", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:7292").AllowCredentials();
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:65515").AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors("wasm");
app.MapIdentityApi<ApplicationUser>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
