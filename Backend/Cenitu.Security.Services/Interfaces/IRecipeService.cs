using Cenitu.Security.Services.Services;

namespace Cenitu.Security.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeListDto>> GetRecipesAsync();
    }
}