using Microsoft.EntityFrameworkCore;
using StoreBPO.Models;

namespace StoreBPO.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreProductMapping> Mappings { get; set; }

    }
}
