using Cenitu.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cenitu.Security.DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OrderNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Date).IsRequired();
            builder.HasOne(x => x.Product).WithMany(x=>x.Orders).HasForeignKey(x => x.ProductId);
        }
    }
}
