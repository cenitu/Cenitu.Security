using AutoMapper;
using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Services.Services
{
    public class RecipeService(AppDbContext context, IMapper mapper) : IRecipeService
    {
        public async Task<IEnumerable<RecipeListDto>> GetRecipesAsync()
        {
            var recipes = await context.Recipes.Include(r => r.Product)
                                    .Include(r => r.RecipeLines).ThenInclude(rl => rl.Product).ThenInclude(p=>p.ProductUnits!).ThenInclude(pu=>pu.Unit)
                                    .Include(r => r.RecipeLines).ThenInclude(rl => rl.Unit)
                                    .ToListAsync();
            return mapper.Map<IEnumerable<RecipeListDto>>(recipes);
        }
    }

    public class RecipeListDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public decimal RecipeWeight { get; set; }
        public List<RecipeLineListDto> RecipeLines { get; set; } = [];
    }

    public class RecipeLineListDto
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string UnitName { get; set; }
        public string UnitSymbol { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public decimal Quantity { get; set; }
        public decimal Weight { get; set; }
        public decimal UnitWeight{ get; set; }
        
    }
}
