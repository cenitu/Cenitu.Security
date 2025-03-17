using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Dtos
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class TransactionLine
    {
        public int Id { get; set; }
        public bool InputOrOutput { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductsId { get; set; }
        public Products Products { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }

    public class Transaction
    {
        public int Id { get; set; }
        public List<TransactionLine> Lines { get; set; }
    }
    public class Sale
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }

    public class ProductionOrders
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public DateTime Date { get; set; }
        public string OperatorName { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
