// BlogApp.Infrastructure/Services/AuthService.cs
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly SignInManager<User> _signInManager;
        public AuthService(
            UserManager<User> userManager,SignInManager<User> signInManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        
        public async Task<(string Token, string RefreshToken, DateTime Expiration)> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                throw new ApplicationException("User already exists.");

            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new ApplicationException(string.Join(", ", result.Errors.Select(e => e.Description)));

            return await GenerateTokensAsync(user);
        }

        public async Task<(string Token, string RefreshToken, DateTime Expiration)> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new ApplicationException("Invalid login attempt.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new ApplicationException("Invalid login attempt.");

            return await GenerateTokensAsync(user);
        }

        public async Task<(string Token, string RefreshToken, DateTime Expiration)> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Token); // normally you'd parse claims
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new ApplicationException("Invalid refresh token.");

            return await GenerateTokensAsync(user);
        }

        private async Task<(string Token, string RefreshToken, DateTime Expiration)> GenerateTokensAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var jwtToken = _jwtTokenGenerator.GenerateToken(user, roles);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var tokenObj = handler.ReadJwtToken(jwtToken);

            return (jwtToken, refreshToken, tokenObj.ValidTo);
        }
    }
}