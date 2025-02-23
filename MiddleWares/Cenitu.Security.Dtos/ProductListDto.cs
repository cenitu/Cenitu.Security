using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Dtos
{
    public class ProductListDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public string PrimaryUnitSymbol { get; set; }   
        //public string PrimaryUnitSymbol => ProductUnits.FirstOrDefault(pu => pu.IsPrimary)?.Unit?.Symbol;
        //public List<ProductUnitDto> ProductUnits { get; set; } = new();
    }
}
