// BlogApp.Application/Interfaces/IBlogRepository.cs
public interface IBlogRepository
{
    Task<Blog?> GetByIdAsync(int id);
    Task<IEnumerable<Blog>> GetAllAsync();
    Task AddAsync(Blog blog);
    Task UpdateAsync(Blog blog);
    Task DeleteAsync(Blog blog);
    Task<bool> IsUserOwnerAsync(int blogId, string userId);
}