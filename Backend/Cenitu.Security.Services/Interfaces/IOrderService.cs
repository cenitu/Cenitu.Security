using Cenitu.Security.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponse<OrderListDto>> GetOrdersAsync(int skip, int? top, string? filter, string? orderby);
    }
}
