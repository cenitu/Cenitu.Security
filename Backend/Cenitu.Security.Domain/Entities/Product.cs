using Cenitu.Security.Dtos.Enums;

namespace Cenitu.Security.Domain.Entities
{
    public class Product : TracedBase
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ProductType ProductType { get; set; }
        public ProductTrackingType TrackingType { get; set; }
        public ICollection<ProductionOrder>? Orders { get; set; } = [];
        public ICollection<ProductUnit>? ProductUnits { get; set; } = [];
        public Unit? PrimaryUnit
        {
            get
            {
                var primaryUnit = ProductUnits?.FirstOrDefault(pu => pu.IsPrimary);
                return primaryUnit?.Unit;
            }
        }
        public string? PrimaryUnitSymbol
        {
            get
            {
                return PrimaryUnit?.Symbol;
            }
        }

    }

}
