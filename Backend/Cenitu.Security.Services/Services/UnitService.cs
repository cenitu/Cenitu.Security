using AutoMapper;
using Cenitu.Security.DataAccess;
using Cenitu.Security.Domain.Entities;
using Cenitu.Security.Dtos;
using Cenitu.Security.Dtos.Product;
using Cenitu.Security.Dtos.Unit;
using Cenitu.Security.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cenitu.Security.Services.Services
{
    public class UnitService : IUnitService
    {
        private readonly IMapper mapper;

        private readonly AppDbContext context;

        public UnitService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }



        public async Task<ApiResponse<UnitDto>> GetUnitsAsync(int skip, int? top, string? filter, string? orderby)
        {
            var query = context.Units.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                filter = CenituServiceHelpers.RefineFilter(filter);
                query = query.Where(x => x.Symbol.Contains(filter) ||
                                         x.Name.Contains(filter));
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                var orderBys = orderby.Split(' ');

                
                    query = orderBys.Length == 2
                        ? query.OrderByDescending(x => EF.Property<object>(x, orderBys[0]))
                        : query.OrderBy(x => EF.Property<object>(x, orderBys[0]));
                
            }
            var count = await query.CountAsync();
            if (top.HasValue)
            {
                query = query.Skip(skip).Take(top.Value);
            }
            else
            {
                query = query.Skip(skip);
            }

            var units = await query.ToListAsync();


            var unitList = mapper.Map<List<UnitDto>>(units);



            return new ApiResponse<UnitDto>
            {
                Count = count,
                Items = unitList
            };
        }

        public async Task<UnitDto> GetUnitAsync(int Id)
        {
            var unit = await context.Units.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (unit == null)
            {
                throw new Exception("Product not found");
            }
            return mapper.Map<UnitDto>(unit);
        }

        public async Task<UnitDto> UpdateUnitAsync(UnitDto unitDto)
        {
            var unit = await context.Units.Where(x => x.Id == unitDto.Id).FirstOrDefaultAsync();
            if (unit == null)
            {
                throw new Exception("Product not found");
            }
            unit.Name = unitDto.Name;
            unit.Symbol = unitDto.Symbol;
            context.Units.Update(unit);
            await context.SaveChangesAsync();
            return mapper.Map<UnitDto>(unit);
        }
    }
}
