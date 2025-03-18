using System.ComponentModel.DataAnnotations;

namespace Cenitu.Security.Dtos.Recipe
{
    public class RecipeLineListDto
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string UnitName { get; set; }
        public string UnitSymbol { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public decimal Quantity { get; set; }
        public decimal Weight { get; set; }
        public decimal UnitWeight{ get; set; }
        
    }
}
