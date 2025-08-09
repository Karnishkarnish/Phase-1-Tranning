using Microsoft.EntityFrameworkCore;

namespace Project.Models
{
    public class SpendSmartDbContext : DbContext
    {
        public DbSet<Expence> Expences { get; set; }
        public SpendSmartDbContext(DbContextOptions<SpendSmartDbContext> options) : base(options) { }
    }
}
