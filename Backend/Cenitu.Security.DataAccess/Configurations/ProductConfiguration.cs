using Cenitu.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Code).HasMaxLength(20).IsRequired();
            builder.HasIndex(p=> p.Code).IsUnique();
            builder.Property(p => p.Description).HasMaxLength(200).IsRequired();
            builder.Property(p => p.ProductType).HasConversion<byte>();
            builder.Property(p => p.TrackingType).HasConversion<byte>();
            builder.Property(p => p.StockQuantity).HasPrecision(19, 3);
            builder.Property(p => p.SellingPrice).HasPrecision(18, 3);
            


            //builder.Property(p=>p.PrimaryUnitSymbol).HasMaxLength(10);
            builder.ToTable("Products");
        }
    }
}
