using Cenitu.Security.Dtos.Unit;

namespace Cenitu.Security.Services.Interfaces
{
    public interface IUnitService
    {
        Task<List<UnitDto>> GetUnitsAsync();
    }
}