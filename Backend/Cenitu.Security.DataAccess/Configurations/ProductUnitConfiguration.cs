using Cenitu.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cenitu.Security.DataAccess.Configurations
{
    public class ProductUnitConfiguration : IEntityTypeConfiguration<ProductUnit>
    {
        public void Configure(EntityTypeBuilder<ProductUnit> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.UnitId });
            builder.HasIndex(x=>new { x.ProductId, x.UnitId }).IsUnique();
            builder.HasOne(x => x.Product).WithMany(x => x.ProductUnits).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Unit).WithMany(x=>x.ProductUnits).HasForeignKey(x => x.UnitId);
            builder.Property(x=>x.ConversionFactor).HasPrecision(18, 3);
            builder.Property(x => x.Weight).HasPrecision(18, 3);
            builder.ToTable("ProductUnits");

        }
    }
}
