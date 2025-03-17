using Cenitu.Security.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cenitu.Security.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService recipeService;
        public RecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await recipeService.GetRecipesAsync();
            return Ok(result);
        }
    }
}
