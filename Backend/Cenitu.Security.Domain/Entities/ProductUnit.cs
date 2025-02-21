using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Domain.Entities
{
    public class ProductUnit
    {
        [Key]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        [Key]
        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null!;

        public decimal ConversionFactor { get; set; } // Ana birime dönüşüm faktörü
        public bool IsPrimary { get; set; } // Bu birim ana birim mi?
    }
}
