using Intranet.Data;
using Intranet.Models;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Services;

public class MenuItemService(IDbContextFactory<AppDbContext> dbFactory) : IMenuService
{
    public async Task<List<MenuItem>> GetMenuTreeAsync()
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var flatItems = await db.MenuItems
            .AsNoTracking()
            .Where(m => m.IsActive)
            .OrderBy(m => m.SortOrder)
            .ToListAsync();

        return BuildTree(flatItems);
    }

    private static List<MenuItem> BuildTree(List<MenuItem> flatItems)
    {
        var itemsById = flatItems.ToDictionary(m => m.Id);

        foreach (var item in flatItems)
        {
            if (item.ParentId is null)
                continue;

            if (itemsById.TryGetValue(item.ParentId.Value, out var parent))
            {
                parent.Children.Add(item);
            }
        }

        foreach (var item in flatItems)
        {
            item.Children = item.Children.OrderBy(c => c.SortOrder).ToList();
        }

        return flatItems
            .Where(m => m.ParentId == null)
            .OrderBy(m => m.SortOrder)
            .ToList();
    }

    public async Task<List<MenuItem>> GetAllMenuItemsAsync()
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        return await db.MenuItems
            .AsNoTracking()
            .OrderBy(m => m.SortOrder)
            .ThenBy(m => m.Title)
            .ToListAsync();
    }

    public async Task<MenuItem> CreateMenuItemAsync(MenuItem item)
    {
        NormalizeMenuItem(item);

        await using var db = await dbFactory.CreateDbContextAsync();
        db.MenuItems.Add(item);
        await db.SaveChangesAsync();
        return item;
    }

    public async Task<MenuItem> UpdateMenuItemAsync(MenuItem item)
    {
        NormalizeMenuItem(item);

        if (item.Id <= 0)
            throw new ArgumentException("A valid menu item must be selected.");

        if (item.ParentId == item.Id)
            throw new ArgumentException("A menu item cannot be its own parent.");

        await using var db = await dbFactory.CreateDbContextAsync();
        var existing = await db.MenuItems.FindAsync(item.Id)
            ?? throw new InvalidOperationException("Menu item not found.");

        existing.Title = item.Title;
        existing.Url = item.Url;
        existing.Icon = item.Icon;
        existing.ParentId = item.ParentId;
        existing.SortOrder = item.SortOrder;
        existing.IsActive = item.IsActive;

        await db.SaveChangesAsync();
        return existing;
    }

    private static void NormalizeMenuItem(MenuItem item)
    {
        var title = item.Title?.Trim();
        if (string.IsNullOrEmpty(title))
            throw new ArgumentException("Title is required.");

        item.Title = title;
        item.Url = string.IsNullOrWhiteSpace(item.Url) ? null : item.Url.Trim();
        item.Icon = string.IsNullOrWhiteSpace(item.Icon) ? null : item.Icon.Trim();
    }
}
