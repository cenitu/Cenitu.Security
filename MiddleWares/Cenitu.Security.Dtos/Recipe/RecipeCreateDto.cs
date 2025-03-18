namespace Cenitu.Security.Dtos.Recipe
{
    public class RecipeCreateDto
    {
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int ProductId { get; set; }
        public decimal RecipeWeight { get;  }
        public List<RecipeLineCreateDto> RecipeLines { get; set; } = [];
    }
}
