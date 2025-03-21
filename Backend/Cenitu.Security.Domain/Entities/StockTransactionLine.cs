using Cenitu.Security.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Domain.Entities
{
    public class StockTransactionLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal TransactionUnitQuantity { get; set; }
        public decimal Price { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }

        public TransactionType TransactionType { get; set; }

        public int StockTransactionId { get; set; }
        public decimal ConversionFactor
        {
            get
            {
                return Product.ProductUnits!.FirstOrDefault(pu => pu.UnitId == UnitId)?.ConversionFactor ?? 1;
            }
        }
        public string TransactionUnitSymbol
        {
            get
            {
                return Unit.Symbol;
            }
        }

        public string PrimaryUnitSymbol
        {
            get
            {
                return Product.PrimaryUnitSymbol!;
            }
        }
        public decimal PrimaryUnitQuantity
        {
            get
            {
                return TransactionUnitQuantity * ConversionFactor;
            }
        }
        public StockTransaction StockTransaction { get; set; }



    }



}
