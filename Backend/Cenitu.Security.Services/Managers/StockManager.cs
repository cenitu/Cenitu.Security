using AutoMapper;
using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Services.Managers
{
    public class StockManager
    {
        private AppDbContext _appDbContext;
        private IMapper mapper;

        public StockManager(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            this.mapper = mapper;
        }

        //public async Task<List<Product>> UpdateStocks(List<Product> productIds)
        //{
        //    var products = _appDbContext.Products.Where(p => productIds.Contains(p.Id)).ToList();
        //    foreach (var product in products)
        //    {
        //        product.StockQuantity = product.StockTransactionLines.Sum(x => x.TransactionUnitQuantity);
        //    }
        //    await _appDbContext.SaveChangesAsync();
        //}
    }
}
