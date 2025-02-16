using AutoMapper;
using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos;
using Cenitu.Security.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<ProductListDto>> GetProductsAsync()
        {
            var productList = await context.Products.ToListAsync();
            return mapper.Map<List<ProductListDto>>(productList);
        }

        public async Task<ProductAddDto> AddProductAsync(ProductAddDto productAddDto)
        {
            var product = mapper.Map<Product>(productAddDto);
            context.Products.Add(product);
            var result = await context.SaveChangesAsync();
            if (result == 0)
            {
                throw new Exception("Product not added");
            }
            return mapper.Map<ProductAddDto>(product);
        }

        public async Task<PagedAndSortedResult<ProductListDto>> GetProductsPaged(int page = 1, int pageSize = 10, string sortColumn = "Id", string sortDirection = "asc")
        {
            var query = context.Products.AsQueryable();

            // Sıralama Uygula
            query = sortColumn.ToLower() switch
            {
                "code" => sortDirection == "asc" ? query.OrderBy(p => p.Code) : query.OrderByDescending(p => p.Code),
                "description" => sortDirection == "asc" ? query.OrderBy(p => p.Description) : query.OrderByDescending(p => p.Description),
                _ => sortDirection == "asc" ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id)
            };

            // Toplam kayıt sayısını al
            var totalCount = await query.CountAsync();

            // Sayfalama uygula
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Code = p.Code,
                    Description = p.Description
                })
                .ToListAsync();

            


            // Sayfalama sonucu döndür
            var pagedAndSortedList = new PagedAndSortedResult<ProductListDto>
            {
                Data = products,
                TotalCount = totalCount,
                PageIndex = page,
                PageSize = pageSize

            };
            return pagedAndSortedList;
        }
    }
}
