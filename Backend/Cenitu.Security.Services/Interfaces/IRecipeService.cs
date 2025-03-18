using Cenitu.Security.Dtos;
using Cenitu.Security.Dtos.Recipe;

namespace Cenitu.Security.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<RecipeCreateDto> CreateRecipeAsync(RecipeCreateDto recipeDto);
        Task<RecipeListDto> GetRecipeAsync(int id);
        Task<ApiResponse<RecipeListDto>> GetRecipesAsync(int skip, int? top, string? filter, string? orderby);
    }
}