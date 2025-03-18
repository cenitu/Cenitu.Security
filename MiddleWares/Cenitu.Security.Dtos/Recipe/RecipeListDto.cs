namespace Cenitu.Security.Dtos.Recipe
{
    public class RecipeListDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public decimal RecipeWeight { get; set; }
        //public List<RecipeLineListDto> RecipeLines { get; set; } = [];
    }
}
