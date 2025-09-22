// BlogApp.Domain/Entities/User.cs
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public ICollection<ProjectOwner> ProjectOwners { get; set; } = new List<ProjectOwner>();
}
/*
public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public ICollection<ProjectOwner> ProjectOwners { get; set; } = new List<ProjectOwner>();
}
*/