using IPInfo.Core.Models;
using IPInfo.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace IPInfo.Data
{
    public class IPInfoDbContext : DbContext
    {
        public DbSet<IP> IPDetails { get; set; }

        public IPInfoDbContext(DbContextOptions<IPInfoDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new IPConfiguration());
        }
    }
}
