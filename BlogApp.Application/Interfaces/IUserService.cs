public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync(string currentUserId);
}