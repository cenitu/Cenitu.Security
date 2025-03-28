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
    public class StockTransactionLineConfiguration : IEntityTypeConfiguration<StockTransactionLine>
    {
        public void Configure(EntityTypeBuilder<StockTransactionLine> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Price).HasPrecision(18, 4);
            builder.Property(s => s.TransactionUnitQuantity).HasPrecision(18, 4);
            builder.HasOne(s=>s.StockTransaction).WithMany(x=>x.StockTransactions).HasForeignKey(x=>x.StockTransactionId);
            builder.HasOne(s => s.Product).WithMany(x=>x.StockTransactionLines).HasForeignKey(x => x.ProductId);
            builder.HasOne(s=>s.Unit).WithMany().HasForeignKey(x => x.UnitId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(s => s.TransactionType).HasConversion<byte>();
            builder.Property(s=>s.TransactionConversionFactor).HasPrecision(18, 4);
            builder.ToTable("StockTransactionLines");
        }
    }

}
