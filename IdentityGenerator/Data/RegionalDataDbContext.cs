using IdentityGenerator.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityGenerator.Data;

public class RegionalDataDbContext : DbContext
{
    public RegionalDataDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
        Database.Migrate();
    }

    public DbSet<FirstName> FirstNames { get; set; }
    public DbSet<SecondName> SecondNames { get; set; }
    public DbSet<Address> Addresses { get; set; }
}
