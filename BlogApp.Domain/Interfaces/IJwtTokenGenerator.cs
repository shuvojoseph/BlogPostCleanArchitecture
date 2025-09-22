// BlogApp.Domain/Interfaces/IJwtTokenGenerator.cs

namespace BlogApp.Domain.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, IList<string> roles);
        string GenerateRefreshToken();
    }
}