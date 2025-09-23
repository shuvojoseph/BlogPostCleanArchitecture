public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAllUsersExceptAsync(string currentUserId);
}