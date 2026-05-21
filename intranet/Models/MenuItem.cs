namespace Intranet.Models;

public class MenuItem
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string? Icon { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public List<MenuItem>? Children { get; set; } = new();
}