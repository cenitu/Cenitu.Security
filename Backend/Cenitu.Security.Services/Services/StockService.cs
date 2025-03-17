using AutoMapper;
using Cenitu.Security.DataAccess;
using Cenitu.Security.Dtos;
using Cenitu.Security.Dtos.Enums;
using Cenitu.Security.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Services.Services
{
    public class StockService : IStockService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper mapper;

        public StockService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<StockListDto>> GetStocksAsync(int skip, int? top, string? filter, string? orderby)
        {
            var result = await _appDbContext.StockTransactionLines
            .Include(stl => stl.Product)  // Product'ı dahil et
            .GroupBy(stl => new { stl.Product.Code, stl.Product.Description })  // Code ve Description'a göre gruplama
            .Select(g => new StockListDto
            {
                Code = g.Key.Code,
                Description = g.Key.Description,
                TotalQuantity = g.Sum(stl =>
                    stl.TransactionType == TransactionType.Input ? -stl.Quantity :
                    stl.TransactionType == TransactionType.Output ? stl.Quantity : 0)
            }).ToListAsync();
            return new ApiResponse<StockListDto>
            {
                Items = result,
                Count = result.Count
            };
        }
    }

    public class StockListDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal TotalQuantity { get; set; }
    }
}
