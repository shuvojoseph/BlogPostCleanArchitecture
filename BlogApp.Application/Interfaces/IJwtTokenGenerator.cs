// BlogApp.Application/Interfaces/IJwtTokenGenerator.cs

    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, IList<string> roles);
        string GenerateRefreshToken();
    }