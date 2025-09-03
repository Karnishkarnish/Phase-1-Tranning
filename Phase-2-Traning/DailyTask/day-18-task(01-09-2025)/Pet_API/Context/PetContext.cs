using Microsoft.EntityFrameworkCore;
using PetApi.Models;

namespace PetApi.Data;

public class PetContext : DbContext
{
    public PetContext(DbContextOptions<PetContext> options) : base(options) { }

    public DbSet<Pet> Pets { get; set; }
}
