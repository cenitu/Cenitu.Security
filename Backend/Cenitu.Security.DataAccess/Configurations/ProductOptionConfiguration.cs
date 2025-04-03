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
    public class ProductOptionConfiguration : IEntityTypeConfiguration<ProductOption>
    {
        public void Configure(EntityTypeBuilder<ProductOption> builder)
        {
            builder.HasKey(po => new { po.ProductId, po.OptionProductId });
            builder.HasOne(po => po.Product)
                .WithMany(p => p.ProductOptions)
                .HasForeignKey(po => po.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(po => po.OptionProduct)
                .WithMany()
                .HasForeignKey(po => po.OptionProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
