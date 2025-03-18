using AutoMapper;
using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos.Order;
using Cenitu.Security.Dtos;
using Cenitu.Security.Dtos.Recipe;
using Cenitu.Security.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Services.Services
{
    public class RecipeService(AppDbContext context, IMapper mapper) : IRecipeService
    {
        public async Task<ApiResponse<RecipeListDto>> GetRecipesAsync(int skip, int? top, string? filter, string? orderby)
        {
            var recipes = await context.Recipes.Include(r => r.Product)
                                    .Include(r => r.RecipeLines).ThenInclude(rl => rl.Product).ThenInclude(p=>p.ProductUnits!).ThenInclude(pu=>pu.Unit)
                                    .Include(r => r.RecipeLines).ThenInclude(rl => rl.Unit)
                                    .ToListAsync();

            var recipeListDto= mapper.Map<List<RecipeListDto>>(recipes);
            return new ApiResponse<RecipeListDto>
            {
                Count = recipeListDto.Count,
                Items = recipeListDto
            };
        }

        public async Task<RecipeListDto> GetRecipeAsync(int id)
        {
            var recipe = await context.Recipes.Include(r => r.Product)
                                    .Include(r => r.RecipeLines).ThenInclude(rl => rl.Product).ThenInclude(p => p.ProductUnits!).ThenInclude(pu => pu.Unit)
                                    .Include(r => r.RecipeLines).ThenInclude(rl => rl.Unit)
                                    .FirstOrDefaultAsync(r => r.Id == id);
            return mapper.Map<RecipeListDto>(recipe);
        }
        public async Task<RecipeCreateDto> CreateRecipeAsync(RecipeCreateDto recipeDto)
        {
            var recipe = mapper.Map<Recipe>(recipeDto);
            recipe.Product = null;
            foreach (var item in recipe.RecipeLines)
            {
                item.Product = null;
            }
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();
            return mapper.Map<RecipeCreateDto>(recipe);
        }
    }
}
