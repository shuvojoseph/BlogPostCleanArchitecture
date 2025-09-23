public interface IBlogService
    {
        Task<BlogDto> AddBlogAsync(string userId, CreateBlogRequest request);
        Task<BlogDto?> UpdateBlogAsync(int blogId, string userId, UpdateBlogRequest request);
        Task<bool> DeleteBlogAsync(int blogId, string userId);
        Task<IEnumerable<BlogDto>> GetAllBlogsAsync();
        Task<BlogDto?> GetByIdAsync(int blogId);
    }
/*
public interface IBlogService
{
    Task<BlogDto> AddBlogAsync(CreateBlogRequest request, string currentUserId);
    Task<BlogDto?> UpdateBlogAsync(int id, UpdateBlogRequest request, string currentUserId);
    Task<bool> DeleteBlogAsync(int id, string currentUserId);
    Task<IEnumerable<BlogDto>> GetAllBlogsAsync();
    Task<BlogDto?> GetByIdAsync(int blogId);
}
*/