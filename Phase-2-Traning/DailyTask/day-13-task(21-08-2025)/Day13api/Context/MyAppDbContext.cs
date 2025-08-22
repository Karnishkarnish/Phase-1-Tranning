using Day13api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Day13api.Context
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options)
            : base(options)
        {
            

        }
        public DbSet<StateInfo> States { get; set; }
    }
}
