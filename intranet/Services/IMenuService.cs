using Intranet.Models;

namespace Intranet.Services;

public interface IMenuService
{
    Task<List<MenuItem>> GetMenuTreeAsync();
    Task<List<MenuItem>> GetAllMenuItemsAsync();
    Task<MenuItem> CreateMenuItemAsync(MenuItem item);
}
