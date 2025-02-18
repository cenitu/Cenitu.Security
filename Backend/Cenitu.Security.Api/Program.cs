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

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Database Bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 2️⃣ Identity Kullanıcı Yönetimi
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(opts => { })
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// 3️⃣ Servisleri Bağla
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(typeof(MyMapper).Assembly);

//// 4️⃣ OData Modeli Tanımlama
ODataConventionModelBuilder odataBuilder = new ODataConventionModelBuilder();
odataBuilder.EntitySet<Product>("Products"); // OData için "Products" entity'si

IEdmModel edmModel = odataBuilder.GetEdmModel();

//static IEdmModel GetEdmModel()
//{
//    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
//    var books = builder.EntitySet<Product>("Products");
//    FunctionConfiguration myFirstFunction = books.EntityType.Collection.Function("MyFirstFunction");
//    myFirstFunction.ReturnsCollectionFromEntitySet<Product>("Products");
//    return builder.GetEdmModel();
//}

// 5️⃣ OData Konfigürasyonu
builder.Services.AddControllers()
    .AddOData(opt =>
    {
        opt.AddRouteComponents("odata", edmModel) // "api/odata" yerine "odata" route kullanıldı
            .Select()
            .Expand()
            .Filter()
            .OrderBy()
            .SetMaxTop(100) // Maksimum veri limiti
            .Count()
            ;
    });

// 6️⃣ Swagger Ayarları (OData ile uyumlu hale getir)
builder.Services.AddSwaggerGen(opts =>
{
    opts.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    opts.OperationFilter<SecurityRequirementsOperationFilter>();

    // 🌟 OData endpoint çakışmasını önlemek için belirli yolları hariç tut
    opts.DocInclusionPredicate((docName, apiDesc) =>
    {
        return apiDesc.RelativePath != null &&
               !apiDesc.RelativePath.StartsWith("odata/$metadata") &&
               !apiDesc.RelativePath.StartsWith("odata/Products/$count");
    });

    // 🌟 OData Query Parametrelerini Swagger için tanımla
    opts.OperationFilter<ODataQueryOptionsFilter>();
});

// 7️⃣ CORS Ayarları
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("wasm", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:7292").AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors("wasm");
app.MapIdentityApi<ApplicationUser>();

// 8️⃣ Swagger UI & API Dokümantasyonu
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 9️⃣ Middleware & API Route Mapping
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
