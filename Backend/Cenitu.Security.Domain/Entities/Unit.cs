using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Domain.Entities
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Birim adı (kg, m, adet vb.)
        public string Symbol { get; set; } = string.Empty; // Sembol (kg, m, pcs vb.)
        public ICollection<ProductUnit> ProductUnits { get; set; } = [];
    }
}
