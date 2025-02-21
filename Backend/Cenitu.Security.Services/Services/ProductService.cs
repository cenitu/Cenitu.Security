using AutoMapper;
using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos;
using Cenitu.Security.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;
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

        public async Task<ProductListDto> GetProductAsync(int Id)
        {
            var product = await context.Products.Include(x => x.ProductUnits)!.ThenInclude(x => x.Unit).Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return mapper.Map<ProductListDto>(product);
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
            return context.Products;
        }


    }
}
