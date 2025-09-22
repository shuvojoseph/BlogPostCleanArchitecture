// BlogApp.Infrastructure/Repositories/BlogRepository.cs
using Microsoft.EntityFrameworkCore;

public class BlogRepository : IBlogRepository
{
    private readonly ApplicationDbContext _context;

    public BlogRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Blog?> GetByIdAsync(int id)
    {
        return await _context.Blogs
            .Include(b => b.ProjectOwners)
            .ThenInclude(po => po.User)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Blog>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Blog blog)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Blog blog)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Blog blog)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsUserOwnerAsync(int blogId, string userId)
    {
        throw new NotImplementedException();
    }
}