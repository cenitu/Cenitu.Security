using AutoMapper;
using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos;
using Cenitu.Security.Dtos.Enums;
using Cenitu.Security.Dtos.Product;
using Cenitu.Security.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ProductUpdateDto> GetProductAsync(int Id)
        {
            var product = await context.Products
                .Include(x => x.ProductUnits)!
                .ThenInclude(x => x.Unit)
                .Include(p => p.ProductOptions)
                .ThenInclude(x => x.OptionProduct)
                .Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return mapper.Map<ProductUpdateDto>(product);
        }

        public async Task<ProductCreateDto> AddProductAsync(ProductCreateDto productCreateDto)
        {
            if (productCreateDto.CreatedByUserName is null)
            {
                throw new Exception("User name is not supplied");
            }
            //var user = await userManager.FindByEmailAsync(productCreateDto.CreatedByUserName) ?? throw new Exception("Un Authorized");
            var product = mapper.Map<Product>(productCreateDto);
            product.CreatedById = productCreateDto.CreatedByUserName;
            //product.CreatedBy = user;
            product.CreatedDate = DateTime.Now;
            product.ProductUnits = [.. productCreateDto.ProductUnits.Select(pu => new ProductUnit
            {
                UnitId = pu.UnitId,
                IsPrimary = pu.IsPrimary,
                ConversionFactor = pu.ConversionFactor
            })];
            //product.ProductUnits = [];
            //foreach (var productUnit in productCreateDto.ProductUnits!)
            //{
            //    product.ProductUnits.Add(new ProductUnit
            //    {
            //        UnitId = productUnit.UnitId,
            //        ConversionFactor = productUnit.ConversionFactor,
            //    });
            //}
            product.ProductOptions = [.. productCreateDto.ProductOptions.Select(po => new ProductOption
            {
                OptionProductId = po.OptionProductId,
            })];
            context.Products.Add(product);

            var result = await context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("Product not added");
            }

            return mapper.Map<ProductCreateDto>(product);
        }


        public async Task<ProductUpdateDto> UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            //var user = await userManager.FindByEmailAsync(productUpdateDto.LastModifiedByUserName!);
            var product = await context.Products.Include(x => x.ProductUnits!).ThenInclude(x => x.Unit).Include(x => x.ProductOptions).FirstOrDefaultAsync(x => x.Id == productUpdateDto.Id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            var productPrimaryUnitId = product.PrimaryUnit!.Id;


            // Güncellenmesi gereken alanları doğrudan var olan nesneye uygula
            mapper.Map(productUpdateDto, product);
            //var productToUpdate = mapper.Map<Product>(productUpdateDto);
            product.ProductUnits = productUpdateDto.ProductUnits.Select(pu => new ProductUnit
            {
                ProductId = product.Id,
                UnitId = pu.UnitId,
                IsPrimary = pu.IsPrimary,
                ConversionFactor = pu.ConversionFactor,
                Weight = pu.Weight
            }).ToList();

            product.LastModifiedById = productUpdateDto.LastModifiedByUserName;
            //product.LastModifiedBy = user;
            product.LastModifiedDate = DateTime.Now;
            //context.Products.Update(productToUpdate);
            // Değişiklikleri kaydet
            product.ProductOptions = productUpdateDto.ProductOptions.Select(po => new ProductOption
            {
                ProductId = product.Id,
                OptionProductId = po.OptionProductId,
            }).ToList();
            var result = await context.SaveChangesAsync();
            if (result == 0)
            {
                throw new Exception("Product not updated");
            }
            if (productPrimaryUnitId != productUpdateDto.ProductUnits.FirstOrDefault(x => x.IsPrimary)!.UnitId)
            {
                var transactionLines = context.StockTransactionLines.Include(x => x.Product).ThenInclude(x => x.ProductUnits).Where(x => x.ProductId == product.Id).ToList();
                product.StockQuantity = transactionLines.Sum(stl =>
                    stl.TransactionType == TransactionType.Output ? -stl.PrimaryUnitQuantity :
                    stl.TransactionType == TransactionType.Input ? stl.PrimaryUnitQuantity : 0);
                await context.SaveChangesAsync();
            }

            // Güncellenmiş haliyle geri dön    
            return mapper.Map<ProductUpdateDto>(product);

            //return productUpdateDto;
        }


        public async Task<ApiResponse<ProductListDto>> GetProductsAsync(int skip, int? top, string? filter, string? orderby)
        {
            var query = context.Products
                .Include(x => x.ProductUnits!).ThenInclude(x => x.Unit)
                .Include(x => x.CreatedBy)
                .Include(x => x.LastModifiedBy)
                .Include(x => x.StockTransactionLines)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                filter = CenituServiceHelpers.RefineFilter(filter);
                query = query.Where(x => x.Code.Contains(filter) ||
                                         x.Description.Contains(filter) ||
                                         x.ProductUnits!.Any(pu => pu.IsPrimary && pu.Unit.Symbol.Contains(filter)));
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                var orderBys = orderby.Split(' ');

                if (orderBys[0] == "PrimaryUnitSymbol")
                {
                    query = orderBys.Length == 2
                        ? query.OrderByDescending(x => x.ProductUnits!.Where(pu => pu.IsPrimary).Select(pu => pu.Unit.Symbol).FirstOrDefault())
                        : query.OrderBy(x => x.ProductUnits!.Where(pu => pu.IsPrimary).Select(pu => pu.Unit.Symbol).FirstOrDefault());
                }
                else
                {
                    query = orderBys.Length == 2
                        ? query.OrderByDescending(x => EF.Property<object>(x, orderBys[0]))
                        : query.OrderBy(x => EF.Property<object>(x, orderBys[0]));
                }
            }

            var count = await query.CountAsync();

            if (top.HasValue)
            {
                query = query.Skip(skip).Take(top.Value);
            }
            else
            {
                query = query.Skip(skip);
            }

            var products = await query.ToListAsync();

      
            var productListDto = mapper.Map<List<ProductListDto>>(products);

           

            return new ApiResponse<ProductListDto>
            {
                Count = count,
                Items = productListDto
            };
        }
        public async Task UpdateProductStocksAsync()
        {
            var products = context.Products
                .Include(x => x.StockTransactionLines)
                .Include(x => x.ProductUnits)!.ThenInclude(x => x.Unit);
            foreach (var product in products)
            {
                product.StockQuantity = product.StockTransactionLines.Sum(stl =>
                    stl.TransactionType == TransactionType.Output ? -stl.PrimaryUnitQuantity :
                    stl.TransactionType == TransactionType.Input ? stl.PrimaryUnitQuantity : 0);
            }
            await context.SaveChangesAsync();
        }
        //public async Task<ApiResponse<ProductListDto>> GetProductsAsync(int skip, int? top, string? filter, string? orderby)
        //{
        //    var query = context.Products
        //        .Include(x => x.ProductUnits!).ThenInclude(x => x.Unit)
        //        .Include(x=>x.CreatedBy)
        //        .Include(x=>x.LastModifiedBy).AsQueryable();
        //    if (!string.IsNullOrEmpty(filter))
        //    {

        //        filter = OrderServiceHelpers.RefineFilter(filter);
        //        query = query.Where(x => x.Code.Contains(filter) || x.Description.Contains(filter) || x.ProductUnits!.Where(x => x.IsPrimary).FirstOrDefault()!.Unit.Symbol.Contains(filter));

        //    }
        //    if (!string.IsNullOrEmpty(orderby))
        //    {
        //        var orderBys = orderby.Split(' ');
        //        var property = typeof(Product).GetProperty(orderBys[0])!;

        //        if (orderBys.Length == 2 && orderBys[0] == "PrimaryUnitSymbol")
        //        {
        //            query = query.OrderByDescending(x => EF.Property<object>(x.ProductUnits!.Where(x => x.IsPrimary).FirstOrDefault()!.Unit, "Symbol"));

        //        }
        //        else if (orderBys[0] == "PrimaryUnitSymbol")
        //        {
        //            query = query.OrderBy(x => EF.Property<object>(x.ProductUnits!.Where(x => x.IsPrimary).FirstOrDefault()!.Unit, "Symbol"));
        //        }
        //        else if (orderBys.Length == 2)
        //        {
        //            query = query.OrderByDescending(x => EF.Property<object>(x, orderBys[0]));
        //        }
        //        else
        //        {
        //            query = query.OrderBy(x => EF.Property<object>(x, orderBys[0]));

        //        }




        //    }
        //    var count = await query.CountAsync();
        //    query = query.Skip(skip);
        //    if (top.HasValue)
        //    {
        //        query = query.Take(top.Value);
        //    }
        //    var products = await query.ToListAsync();
        //    var stockTransactionLines = await context.StockTransactionLines.Where(st=>products.Select(p=>p.Id).ToList().Contains(st.ProductId)).ToListAsync();

        //    var productListDto = mapper.Map<List<ProductListDto>>(products);

        //    foreach (var product in productListDto)
        //    {
        //        product.StockQuantity = stockTransactionLines.Where(stl => stl.ProductId == product.Id).Sum(stl =>
        //            stl.TransactionType == TransactionType.Input ? -stl.Quantity :
        //            stl.TransactionType == TransactionType.Output ? stl.Quantity : 0);
        //    }

        //    return new ApiResponse<ProductListDto>
        //    {
        //        Count = count,
        //        Items = productListDto
        //    };
        //}


        public async Task<ApiResponse<StockListDto>> GetStocksAsync(int skip, int? top, string? filter, string? orderby)
        {
            var result = await context.StockTransactionLines
            .Include(stl => stl.Product)  // Product'ı dahil et
            .GroupBy(stl => new { stl.Product.Code, stl.Product.Description })  // Code ve Description'a göre gruplama
            .Select(g => new StockListDto
            {
                Code = g.Key.Code,
                Description = g.Key.Description,
                TotalQuantity = g.Sum(stl =>
                    stl.TransactionType == TransactionType.Output ? -stl.TransactionUnitQuantity :
                    stl.TransactionType == TransactionType.Input ? stl.TransactionUnitQuantity : 0)
            }).ToListAsync();
            return new ApiResponse<StockListDto>
            {
                Items = result,
                Count = result.Count
            };
        }

    }
}
