using AutoMapper;
using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos;
using Cenitu.Security.Dtos.Enums;
using Cenitu.Security.Dtos.Order;
using Cenitu.Security.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cenitu.Security.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper mapper;

        public OrderService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            this.mapper = mapper;
        }
        public async Task AddOrderAsync(ProductionOrderCreateDto orderCreateDto)
        {
            var order=mapper.Map<ProductionOrder>(orderCreateDto);
                _appDbContext.Orders.Add(order);
            order.StockTransaction.TransactionSource = TransactionSource.Production;
            order.StockTransaction.Date = order.Date;

            await _appDbContext.SaveChangesAsync();
        }
        public async Task<ApiResponse<OrderListDto>> GetOrdersAsync(int skip, int? top, string? filter, string? orderby)
        {
            var query = _appDbContext.Orders.Include(x => x.Product).Include(x=>x.StockTransaction.StockTransactions).ThenInclude(x=>x.Product).AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                filter = OrderServiceHelpers.RefineFilter(filter);
                query = query.Where(x => x.OrderNumber.Contains(filter) || x.Product.Code.Contains(filter) || x.Product.Description.Contains(filter));
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                var orderBys = orderby.Split(' ');
                var property = typeof(ProductionOrder).GetProperty(orderBys[0])!;

                if (orderBys.Length == 2)
                {
                    query = query.OrderByDescending(x => EF.Property<object>(x, orderBys[0]));
                }
                else
                {
                    query = query.OrderBy(x => EF.Property<object>(x, orderBys[0]));

                }

            }

            var count = await query.CountAsync();
            query = query.Skip(skip);
            if (top.HasValue)
            {
                query = query.Take(top.Value);
            }
            var orders = await query.ToListAsync();
            var orderListDto = mapper.Map<List<OrderListDto>>(orders);
            return new ApiResponse<OrderListDto>
            {
                Count = count,
                Items = orderListDto
            };

        }
        public async Task DeleteOrderAsync(int id)
        {
            var order = await _appDbContext.Orders.FindAsync(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            var transaction = await _appDbContext.StockTransactions.FindAsync(order.StockTransactionId);
            _appDbContext.StockTransactions.Remove(transaction!);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
