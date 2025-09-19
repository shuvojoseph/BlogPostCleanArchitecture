// BlogApp.Domain/Entities/Blog.cs
public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public DateTime LastUpdateTime { get; set; } = DateTime.UtcNow;
    public ICollection<ProjectOwner> ProjectOwners { get; set; } = new List<ProjectOwner>();
}