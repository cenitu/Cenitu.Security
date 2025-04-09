using Cenitu.Security.Dtos.Product;
using Cenitu.Security.Dtos;
using Cenitu.Security.Dtos.Unit;

namespace Cenitu.Security.Services.Interfaces
{
    public interface IUnitService
    {
        Task<UnitDto> GetUnitAsync(int Id);
        Task<ApiResponse<UnitDto>> GetUnitsAsync(int skip, int? top, string? filter, string? orderby);
        Task<UnitDto> UpdateUnitAsync(UnitDto unitDto);
    }
}