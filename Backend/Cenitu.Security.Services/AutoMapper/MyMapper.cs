using AutoMapper;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos;
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
            CreateMap<Product, ProductAddDto>().ReverseMap();
        }
    }
}
