// BlogApp.Application/Services/AuthService.cs
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(
            UserManager<User> userManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
/*
        public async Task<(string Token, string RefreshToken, DateTime Expiration)> LoginAsync(
            string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            // Replace SignInManager with UserManager's CheckPasswordAsync
            var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!isValidPassword)
                throw new UnauthorizedAccessException("Invalid credentials");

            return await GenerateAuthResult(user);
        }
*/
        public Task<(string Token, string RefreshToken, DateTime Expiration)> LoginAsync(Microsoft.AspNetCore.Identity.Data.LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<(string Token, string RefreshToken, DateTime Expiration)> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }
/*
        public Task<(string Token, string RefreshToken, DateTime Expiration)> RefreshTokenAsync(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }
*/
        public Task<(string Token, string RefreshToken, DateTime Expiration)> RefreshTokenAsync(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<(string Token, string RefreshToken, DateTime Expiration)> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        /*
public Task<(string Token, string RefreshToken, DateTime Expiration)> RegisterAsync(string email, string password, string firstName, string lastName)
{
  throw new NotImplementedException();
}

public Task<(string Token, string RefreshToken, DateTime Expiration)> RegisterAsync(Microsoft.AspNetCore.Identity.Data.RegisterRequest request)
{
  throw new NotImplementedException();
}

private async Task<(string Token, string RefreshToken, DateTime Expiration)> GenerateAuthResult(User user)
{
  var roles = await _userManager.GetRolesAsync(user);
  var token = _jwtTokenGenerator.GenerateToken(user, roles);
  var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

  user.RefreshToken = refreshToken;
  user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
  await _userManager.UpdateAsync(user);

  return (token, refreshToken, DateTime.UtcNow.AddMinutes(60));
}
*/
    }
}