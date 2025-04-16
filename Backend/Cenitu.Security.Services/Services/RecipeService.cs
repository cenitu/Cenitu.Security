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
            var query = context.Recipes
                               .Include(r => r.Product)
                               .Include(r => r.RecipeLines)
                                    .ThenInclude(rl => rl.Product)
                                        .ThenInclude(p => p.ProductUnits!)
                                            .ThenInclude(pu => pu.Unit)
                               .Include(r => r.RecipeLines)
                                    .ThenInclude(rl => rl.Unit)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                filter = CenituServiceHelpers.RefineFilter(filter);
                query = query.Where(x => x.Code.Contains(filter) ||
                                         x.Description.Contains(filter) ||
                                         x.Product.Code.Contains(filter) ||
                                         x.Product.Description.Contains(filter));
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                var orderBys = orderby.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                bool descending = orderBys.Length == 2 && orderBys[1].ToLower() == "desc";
                switch (orderBys[0])
                {
                    case nameof(RecipeListDto.ProductCode):
                        query = descending ? query.OrderByDescending(x => x.Product.Code) : query.OrderBy(x => x.Product.Code);
                        break;
                    case nameof(RecipeListDto.ProductDescription):
                        query = descending ? query.OrderByDescending(x => x.Product.Description) : query.OrderBy(x => x.Product.Description);
                        break;
                    default:
                        query = descending
                            ? query.OrderByDescending(x => EF.Property<object>(x, orderBys[0]))
                            : query.OrderBy(x => EF.Property<object>(x, orderBys[0]));
                        break;
                }
            }
            var count = await query.CountAsync();

            if (top.HasValue)
            {
                query = query.Skip(skip).Take(top.Value);
            }
            else
            {
                query = query.Skip(skip);
            }
            var recipeList = await query.ToListAsync();
            var recipeListDto = mapper.Map<List<RecipeListDto>>(recipeList);
            return new ApiResponse<RecipeListDto>
            {
                Count = count,
                Items = recipeListDto
            };
        }

        public async Task<RecipeCreateDto> GetRecipeAsync(int id)
        {
            var recipe = await context.Recipes.Include(r => r.Product)
                                    .Include(r => r.RecipeLines).ThenInclude(rl => rl.Product).ThenInclude(p => p.ProductUnits!).ThenInclude(pu => pu.Unit)
                                    .Include(r => r.RecipeLines).ThenInclude(rl => rl.Unit)
                                    .FirstOrDefaultAsync(r => r.Id == id);
            return mapper.Map<RecipeCreateDto>(recipe);
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

        public async Task<RecipeCreateDto> UpdateRecipeAsync(RecipeCreateDto recipeDto)
        {
            var recipe = context.Recipes.Include(x => x.RecipeLines).FirstOrDefault(x => x.Id == recipeDto.Id);
            var recipeLinesToDelete = recipe!.RecipeLines.Where(x => !recipeDto.RecipeLines.Select(r => r.Id).Contains(x.Id)).ToList();
            mapper.Map(recipeDto, recipe);
            foreach (var item in recipe.RecipeLines)
            {
                item.RecipeId = recipe.Id;
            }
            context.RecipeLines.RemoveRange(recipeLinesToDelete);

            await context.SaveChangesAsync();
            return mapper.Map<RecipeCreateDto>(recipe);
        }
    }
}
