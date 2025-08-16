using Day6Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Day6Web.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        // Table name
        public DbSet<MyTree> myTrees { get; set; }
    }
}