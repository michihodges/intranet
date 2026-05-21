using Intranet.Models;

namespace Intranet.Services;

public class MenuItemService : IMenuService
{
// load from database (return List<MenuItem>)
// Create structured menu (return another List<MenuItem>)
// Beide Methods aufrufen (verkettung)

    public Task<List<MenuItem>> GetMenuTreeAsync()
    {
        var menuItems = new List<MenuItem>
        {
            new MenuItem
            {
                Id = 1,
                Title = "Dashboard",
                Url = "/",
                Icon = "bi-house-door-nav-menu",
                SortOrder = 1
            },
            new MenuItem
            {
                Id = 2,
                Title = "Documents",
                Icon = "bi-folder-nav-menu",
                SortOrder = 2,
                Children = new List<MenuItem>
                {
                    new MenuItem
                    {
                        Id = 3,
                        Title = "Policies",
                        Url = "/documents/policies",
                        Icon = "bi-file-earmark-text-nav-menu",
                        ParentId = 2,
                        SortOrder = 1
                    },
                    new MenuItem
                    {
                        Id = 4,
                        Title = "Templates",
                        Url = "/documents/templates",
                        Icon = "bi-file-earmark-nav-menu",
                        ParentId = 2,
                        SortOrder = 2
                    }
                }
            }
        };

        return Task.FromResult(menuItems);
    }
}