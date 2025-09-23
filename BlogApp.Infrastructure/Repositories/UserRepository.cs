using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersExceptAsync(string currentUserId)
    {
        return await _userManager.Users
            .Where(u => u.Id != currentUserId)
            .Select(u => new UserDto(u.Id,$"{u.FirstName} {u.LastName}",u.Email)).ToListAsync();
    }
}