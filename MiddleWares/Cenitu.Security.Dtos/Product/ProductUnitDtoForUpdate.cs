using Cenitu.Security.Dtos.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Dtos.Product
{
    public class ProductUnitDtoForUpdate 
    {
        public int UnitId { get; set; }
        public string UnitSymbol { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public decimal ConversionFactor { get; set; }
        public int ProductId { get; set; }
    }
}
