using System.ComponentModel.DataAnnotations;

namespace Cenitu.Security.Domain.Entities
{
    public class RecipeLine
    {
        public int Id { get; set; }
        public bool IsRawMaterialOrSemiFinished { get; set; }
        public bool IsMainIgredientOrPackaging { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public decimal Quantity { get; set; }
        public decimal Weight
        {
            get
            {
                return Quantity * UnitWeight;
            }
        }
        public decimal UnitWeight
        {
            get
            {
                return Product.ProductUnits!.Where(x => x.UnitId == UnitId).FirstOrDefault()!.Weight;
            }
        }

    }
}