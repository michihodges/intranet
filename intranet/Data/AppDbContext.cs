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

        menuItem.HasData(
            new MenuItem
            {
                Id = 1,
                Title = "Dashboard",
                Url = "/",
                Icon = "bi-house-door-nav-menu",
                SortOrder = 1,
                IsActive = true
            },
            new MenuItem
            {
                Id = 2,
                Title = "Documents",
                Url = null,
                Icon = "bi-folder-nav-menu",
                SortOrder = 2,
                IsActive = true
            },
            new MenuItem
            {
                Id = 3,
                ParentId = 2,
                Title = "Policies",
                Url = "/documents/policies",
                Icon = "bi-file-earmark-text-nav-menu",
                SortOrder = 1,
                IsActive = true
            },
            new MenuItem
            {
                Id = 4,
                ParentId = 2,
                Title = "Templates",
                Url = "/documents/templates",
                Icon = "bi-file-earmark-nav-menu",
                SortOrder = 2,
                IsActive = true
            });
    }
}
