using Cenitu.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.DataAccess.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Code).HasMaxLength(20).IsRequired();
            builder.Property(r => r.Description).HasMaxLength(200).IsRequired();
            builder.HasOne(r => r.Product).WithMany().HasForeignKey(r => r.ProductId);
            builder.ToTable("Recipes");
        }


    }
}
