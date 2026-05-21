using Intranet.Models;

namespace Intranet.Services;

public interface IMenuService
{
    Task<List<MenuItem>> GetMenuTreeAsync();
}
