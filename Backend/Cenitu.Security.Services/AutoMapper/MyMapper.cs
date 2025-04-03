using AutoMapper;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos.Order;
using Cenitu.Security.Dtos.Product;
using Cenitu.Security.Dtos.Recipe;
using Cenitu.Security.Dtos.Unit;

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

            CreateMap<ProductionOrder, OrderListDto>().ReverseMap();

            CreateMap<ProductionOrderCreateDto, ProductionOrder>().ReverseMap();

            CreateMap<StockTransaction, StockTransactionDto>().ReverseMap();
            CreateMap<StockTransactionLine, StockTransactionLineDto>().ReverseMap();

            CreateMap<Recipe, RecipeListDto>().ReverseMap();
            CreateMap<RecipeLine, RecipeLineListDto>().ReverseMap();
            CreateMap<RecipeCreateDto, Recipe>().ReverseMap();
            CreateMap<RecipeLineCreateDto, RecipeLine>().ReverseMap();

            CreateMap<ProductOption, ProductOptionDto>().ReverseMap();
        }
    }
}
