// BlogApp.Domain/Entities/ProjectOwner.cs
public class ProjectOwner
{
    public int BlogId { get; set; }
    public Blog Blog { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = null!;
}