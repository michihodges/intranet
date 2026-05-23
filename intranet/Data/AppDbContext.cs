using Intranet.Models;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var menuItem = modelBuilder.Entity<MenuItem>();

        menuItem.ToTable("MenuItems");

        menuItem.HasOne<MenuItem>()
            .WithMany()
            .HasForeignKey(m => m.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        menuItem.HasIndex(m => m.ParentId);
        menuItem.HasIndex(m => m.SortOrder);
    }
}
