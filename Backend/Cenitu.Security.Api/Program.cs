using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Services.Interfaces;
using Cenitu.Security.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityApiEndpoints<ApplicationUser>(opts =>
{
    
}) 
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IRoleService, RoleService>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(opts =>
{
    opts.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In=ParameterLocation.Header,
        Name="Authorization",
        Type=SecuritySchemeType.ApiKey
    });
    opts.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();


app.MapIdentityApi<ApplicationUser>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
