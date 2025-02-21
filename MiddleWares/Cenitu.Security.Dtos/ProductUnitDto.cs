using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Dtos
{
    public class ProductUnitDto
    {
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        public decimal ConversionFactor { get; set; }
        public bool IsPrimary { get; set; }
        public UnitDto Unit { get; set; }
    }
}
