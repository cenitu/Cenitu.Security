using AutoMapper;
using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos;
using Cenitu.Security.Dtos.Product;
using Cenitu.Security.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Cenitu.Security.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly AppDbContext context;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ProductListDto> GetProductAsync(int Id)
        {
            var product = await context.Products.Include(x => x.ProductUnits)!.ThenInclude(x => x.Unit).Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return mapper.Map<ProductListDto>(product);
        }

        public async Task<ProductCreateDto> AddProductAsync(ProductCreateDto productCreateDto)
        {
            var product = mapper.Map<Product>(productCreateDto);
            context.Products.Add(product);
            var result = await context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("Product not added");
            }

            return mapper.Map<ProductCreateDto>(product);
        }


        public async Task<ProductListDto> UpdateProductAsync(ProductListDto productDto)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == productDto.Id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            // Güncellenmesi gereken alanları doğrudan var olan nesneye uygula
            mapper.Map(productDto, product);

            // Değişiklikleri kaydet
            var result = await context.SaveChangesAsync();
            if (result == 0)
            {
                throw new Exception("Product not updated");
            }

            // Güncellenmiş haliyle geri dön
            return mapper.Map<ProductListDto>(product);
        }
        public IQueryable<Product> Get()
        {
            return context.Products.Include(x => x.ProductUnits);
        }

        public async Task<ApiResponse<ProductListDto>> GetProducts(int skip, int top, string? filter, string? orderby)
        {
            var query = context.Products.Include(x => x.ProductUnits).ThenInclude(x => x.Unit).AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                var matches = Regex.Matches(filter, @"'([^']*)'");
                var filterValues = matches.Cast<Match>().Select(m => m.Groups[1].Value).ToList();
                filter = filterValues.First();
                query = query.Where(x => x.Code.Contains(filter) || x.Description.Contains(filter) || x.ProductUnits.Where(x => x.IsPrimary).FirstOrDefault()!.Unit.Symbol.Contains(filter));
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                var orderBys = orderby.Split(' ');
                var property = typeof(Product).GetProperty(orderBys[0])!;

                if (orderBys.Length == 2 && orderBys[0] == "PrimaryUnitSymbol")
                {
                    query = query.OrderByDescending(x => EF.Property<object>(x.ProductUnits.Where(x => x.IsPrimary).FirstOrDefault()!.Unit, "Symbol"));

                }
                else if (orderBys[0] == "PrimaryUnitSymbol")
                {
                    query = query.OrderBy(x => EF.Property<object>(x.ProductUnits.Where(x => x.IsPrimary).FirstOrDefault()!.Unit, "Symbol"));
                }
                else if (orderBys.Length == 2)
                {
                    query = query.OrderByDescending(x => EF.Property<object>(x, orderBys[0]));
                }
                else
                {
                    query = query.OrderBy(x => EF.Property<object>(x, orderBys[0]));

                }




            }
            var count = await query.CountAsync();
            var products = await query.Skip(skip).Take(top).ToListAsync();
            var productListDto = mapper.Map<List<ProductListDto>>(products);
            return new ApiResponse<ProductListDto>
            {
                Count = count,
                Items = productListDto
            };
        }




    }
}
