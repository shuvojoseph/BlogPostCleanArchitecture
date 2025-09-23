// BlogApp.Application/DTOs/AuthDtos.cs
    public record RegisterRequest(string Email, string Password, string FirstName, string LastName);
    public record LoginRequest(string Email, string Password);
    public record AuthResult(string Token, string RefreshToken, DateTime Expiration);

    public record RefreshTokenRequest(string Token, string RefreshToken);