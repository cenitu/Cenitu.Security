using Cenitu.Security.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Domain.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public decimal RecipeWeight
        {
            get
            {
                return RecipeLines.Sum(rl => rl.Weight);
            }
        }
        public ICollection<RecipeLine> RecipeLines { get; set; } = [];
    }
}
