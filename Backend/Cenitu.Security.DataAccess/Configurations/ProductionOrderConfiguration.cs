using Cenitu.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Transactions;

namespace Cenitu.Security.DataAccess.Configurations
{
    public class ProductionOrderConfiguration : IEntityTypeConfiguration<ProductionOrder>
    {
        public void Configure(EntityTypeBuilder<ProductionOrder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OrderNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Date).IsRequired();
            builder.HasOne(x => x.Product).WithMany(x=>x.Orders).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.StockTransaction).WithOne().HasForeignKey<ProductionOrder>(x=>x.StockTransactionId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Orders");
        }
    }
}
