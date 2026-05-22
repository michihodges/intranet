using Intranet.Data;
using Intranet.Models;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Services;

public class MenuItemService(AppDbContext db) : IMenuService
{
    public async Task<List<MenuItem>> GetMenuTreeAsync()
    {
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
}
