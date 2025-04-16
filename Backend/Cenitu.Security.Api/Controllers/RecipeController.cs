using Cenitu.Security.Dtos.Recipe;
using Cenitu.Security.Services.Interfaces;
using Cenitu.Security.Services.Services;
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
        public async Task<IActionResult> Get([FromQuery(Name = "$inlinecount")] string? inlinecount,
            [FromQuery(Name = "$skip")] int skip,
            [FromQuery(Name = "$top")] int? top,
            [FromQuery(Name = "$filter")] string? filter,
            [FromQuery(Name = "$orderby")] string? orderby)
        {
            var result = await recipeService.GetRecipesAsync(skip, top, filter, orderby);
            if (inlinecount == null)
            {
                return Ok(result.Items);
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await recipeService.GetRecipeAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RecipeCreateDto recipeDto)
        {
            var result = await recipeService.CreateRecipeAsync(recipeDto);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RecipeCreateDto recipeDto)
        {
            var result = await recipeService.UpdateRecipeAsync(recipeDto);
            return Ok(result);
        }
    }
}
