// BlogApp.Application/Interfaces/IAuthService.cs

using Microsoft.AspNetCore.Identity.Data;

public interface IAuthService
{
    Task<(string Token, string RefreshToken, DateTime Expiration)> RegisterAsync(RegisterRequest request);
    Task<(string Token, string RefreshToken, DateTime Expiration)> LoginAsync(LoginRequest request);
    Task<(string Token, string RefreshToken, DateTime Expiration)> RefreshTokenAsync(RefreshTokenRequest request);
}