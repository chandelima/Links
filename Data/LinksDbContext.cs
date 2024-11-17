using Links.Data.Mapeamentos;
using Links.Models;
using Microsoft.EntityFrameworkCore;

namespace Links.Data;

public class LinksDbContext : DbContext
{
    public DbSet<RedirectItem> RedirectItems { get; set; }

    public LinksDbContext(DbContextOptions<LinksDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RedirectItemMap());
    }
}
