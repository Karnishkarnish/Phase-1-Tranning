using Microsoft.EntityFrameworkCore;
using MiniProject.Models;

namespace MiniProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Audience> Audiences { get; set; }
    }
}
