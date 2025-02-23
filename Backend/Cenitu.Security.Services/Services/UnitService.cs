using AutoMapper;
using Cenitu.Security.DataAccess;
using Cenitu.Security.Dtos.Unit;
using Cenitu.Security.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<UnitDto>> GetUnitsAsync()
        {
            var units = await context.Units.ToListAsync();

            return mapper.Map<List<UnitDto>>(units);
        }
    }
}
