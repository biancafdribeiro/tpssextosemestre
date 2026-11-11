using Microsoft.EntityFrameworkCore;
using Patrimonio.Api.Models;

namespace Patrimonio.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Bem> Bens => Set<Bem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bem>()
            .HasIndex(b => b.Tombamento)
            .IsUnique();
    }
}
