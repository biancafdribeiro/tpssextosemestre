using BlContainers.Models;
using Microsoft.EntityFrameworkCore;

namespace BlContainers.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<BL> BLs { get; set; }
    public DbSet<Container> Containers { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Container>()
          .HasOne(c => c.BL)
          .WithMany(b => b.Containers)
          .HasForeignKey(c => c.BLId)
          .OnDelete(DeleteBehavior.Restrict);
    }
}
