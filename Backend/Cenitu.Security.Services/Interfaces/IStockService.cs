using Cenitu.Security.Dtos;
using Cenitu.Security.Services.Services;

namespace Cenitu.Security.Services.Interfaces
{
    public interface IStockService
    {
        Task<ApiResponse<StockListDto>> GetStocksAsync(int skip, int? top, string? filter, string? orderby);
    }
}