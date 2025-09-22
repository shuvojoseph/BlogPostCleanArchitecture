// BlogApp.Application/DTOs/AuthDtos.cs
public record RegisterRequest(string Email, string Password, string FirstName, string LastName);
public record LoginRequest(string Email, string Password);
public record RefreshTokenRequest(string Token, string RefreshToken);
public record AuthResult(string Token, string RefreshToken, DateTime Expiration);

// BlogApp.Application/DTOs/BlogDtos.cs
public record BlogDto(int Id, string Title, string Details, DateTime LastUpdateTime, List<UserDto> Owners);
public record UserDto(string Id, string Name, string Email);
public record CreateBlogRequest(string Title, string Details, List<string> CoOwnerIds);
public record UpdateBlogRequest(string Title, string Details, List<string> CoOwnerIds);