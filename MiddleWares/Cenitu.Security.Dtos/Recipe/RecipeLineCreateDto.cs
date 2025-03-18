using Cenitu.Security.Dtos.Product;
using System.ComponentModel.DataAnnotations;

namespace Cenitu.Security.Dtos.Recipe
{
    public class RecipeLineCreateDto
    {
        public int ProductId { get; set; }
        public ProductListDto Product { get; set; }
        public int UnitId { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public decimal Quantity { get; set; }
    }
}
