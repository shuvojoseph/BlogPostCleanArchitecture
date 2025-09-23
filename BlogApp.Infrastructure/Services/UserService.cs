public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync(string currentUserId)
    {
        return await _userRepository.GetAllUsersExceptAsync(currentUserId);
    }
}