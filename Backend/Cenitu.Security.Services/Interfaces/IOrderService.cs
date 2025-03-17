using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos;
using Cenitu.Security.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(ProductionOrderCreateDto order);
        Task DeleteOrderAsync(int id);
        Task<ApiResponse<OrderListDto>> GetOrdersAsync(int skip, int? top, string? filter, string? orderby);
    }
}
