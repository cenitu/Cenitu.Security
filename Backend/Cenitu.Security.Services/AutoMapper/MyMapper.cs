using AutoMapper;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos.Order;
using Cenitu.Security.Dtos.Product;
using Cenitu.Security.Dtos.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Services.AutoMapper
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<Product, ProductListDto>().ReverseMap();
            //CreateMap<Product, ProductAddDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            //CreateMap<ProductUnit, ProductUnitDto>().ReverseMap();
            CreateMap<Unit,UnitDto>().ReverseMap();
            //CreateMap<ProductUnit, ProductUnitDtoOld>().ReverseMap();
            CreateMap<ProductUnit, ProductUnitDto>().ReverseMap();

            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();

            CreateMap<Order, OrderListDto>().ReverseMap();

        }
    }
}
