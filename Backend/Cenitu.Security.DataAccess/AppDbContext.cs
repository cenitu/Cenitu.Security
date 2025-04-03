using Cenitu.Security.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cenitu.Security.DataAccess
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {


        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
        public DbSet<ProductionOrder> Orders { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<StockTransactionLine> StockTransactionLines { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeLine> RecipeLines { get; set; }
        


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
