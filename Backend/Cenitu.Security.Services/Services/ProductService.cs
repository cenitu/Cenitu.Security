using AutoMapper;
using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos;
using Cenitu.Security.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
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
    }
}
