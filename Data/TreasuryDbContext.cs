using Microsoft.EntityFrameworkCore;
using TreasuryManagementAPI.Models;

namespace TreasuryManagementAPI.Data
{
    public class TreasuryDbContext : DbContext
    {
        public TreasuryDbContext(DbContextOptions<TreasuryDbContext> options) : base(options) {
        
        }
        public DbSet<Collateral> Collaterals { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
