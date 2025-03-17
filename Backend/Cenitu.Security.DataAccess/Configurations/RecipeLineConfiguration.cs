using Cenitu.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cenitu.Security.DataAccess.Configurations
{
    public class RecipeLineConfiguration : IEntityTypeConfiguration<RecipeLine>
    {
        public void Configure(EntityTypeBuilder<RecipeLine> builder)
        {
            builder.HasKey(rl => rl.Id);
            builder.Property(rl => rl.Quantity).HasPrecision(18,3);
            builder.HasOne(rl => rl.Product).WithMany().HasForeignKey(rl => rl.ProductId).OnDelete(DeleteBehavior.Restrict); 
            builder.HasOne(rl => rl.Recipe).WithMany(r => r.RecipeLines).HasForeignKey(rl => rl.RecipeId);
            builder.HasOne(rl => rl.Unit).WithMany().HasForeignKey(rl => rl.UnitId).OnDelete(DeleteBehavior.Restrict); 
            builder.ToTable("RecipeLines");
        }
    }
}
