using Cenitu.Security.Dtos;
using Cenitu.Security.Dtos.Recipe;

namespace Cenitu.Security.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<RecipeCreateDto> CreateRecipeAsync(RecipeCreateDto recipeDto);
        Task<RecipeCreateDto> GetRecipeAsync(int id);
        Task<ApiResponse<RecipeListDto>> GetRecipesAsync(int skip, int? top, string? filter, string? orderby);
        Task<RecipeCreateDto> UpdateRecipeAsync(RecipeCreateDto recipeDto);
    }
}