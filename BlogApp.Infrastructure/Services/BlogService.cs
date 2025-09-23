//using BlogApp.Application.Interfaces;
//using BlogApp.Domain.Entities;

using Microsoft.AspNetCore.Identity;

namespace BlogApp.Infrastructure.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly UserManager<User> _userManager;

        public BlogService(IBlogRepository blogRepository, UserManager<User> userManager)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
        }

        public async Task<BlogDto> AddBlogAsync(string userId, CreateBlogRequest request)
        {
            var blog = new Blog
            {
                Title = request.Title,
                Details = request.Details,
                LastUpdateTime = DateTime.UtcNow
            };

            // Add current user as owner
            blog.ProjectOwners.Add(new ProjectOwner
            {
                UserId = userId
            });

            // Add co-owners
            if (request.CoOwnerIds != null)
            {
                foreach (var coOwnerId in request.CoOwnerIds.Distinct())
                {
                    if (coOwnerId != userId)
                    {
                        blog.ProjectOwners.Add(new ProjectOwner { UserId = coOwnerId });
                    }
                }
            }

            await _blogRepository.AddAsync(blog);

            return await GetByIdAsync(blog.Id) ?? throw new Exception("Failed to create blog");
        }

        public async Task<BlogDto?> UpdateBlogAsync(int blogId, string userId, UpdateBlogRequest request)
        {
            if (!await _blogRepository.IsUserOwnerAsync(blogId, userId))
                throw new UnauthorizedAccessException("You are not an owner of this blog");

            var blog = await _blogRepository.GetByIdAsync(blogId);
            if (blog == null) return null;

            blog.Title = request.Title;
            blog.Details = request.Details;
            blog.LastUpdateTime = DateTime.UtcNow;

            // Reset owners
            blog.ProjectOwners.Clear();

            // Add current user as owner
            blog.ProjectOwners.Add(new ProjectOwner { BlogId = blog.Id, UserId = userId });

            // Add co-owners
            if (request.CoOwnerIds != null)
            {
                foreach (var coOwnerId in request.CoOwnerIds.Distinct())
                {
                    if (coOwnerId != userId)
                    {
                        blog.ProjectOwners.Add(new ProjectOwner { BlogId = blog.Id, UserId = coOwnerId });
                    }
                }
            }

            await _blogRepository.UpdateAsync(blog);

            return await GetByIdAsync(blog.Id);
        }

        public async Task<bool> DeleteBlogAsync(int blogId, string userId)
        {
            if (!await _blogRepository.IsUserOwnerAsync(blogId, userId))
                throw new UnauthorizedAccessException("You are not an owner of this blog");

            var blog = await _blogRepository.GetByIdAsync(blogId);
            if (blog == null) return false;

            await _blogRepository.DeleteAsync(blog);
            return true;
        }

        public async Task<IEnumerable<BlogDto>> GetAllBlogsAsync()
        {
            var blogs = await _blogRepository.GetAllAsync();

            return blogs.Select(b => new BlogDto(
                b.Id,
                b.Title,
                b.Details,
                b.LastUpdateTime,
                b.ProjectOwners.Select(po =>
                    new UserDto(po.UserId, po.User.UserName ?? "", po.User.Email ?? ""))
                    .ToList()
            ));
        }

        public async Task<BlogDto?> GetByIdAsync(int blogId)
        {
            var blog = await _blogRepository.GetByIdAsync(blogId);
            if (blog == null) return null;

            return new BlogDto(
                blog.Id,
                blog.Title,
                blog.Details,
                blog.LastUpdateTime,
                blog.ProjectOwners.Select(po =>
                    new UserDto(po.UserId, po.User.UserName ?? "", po.User.Email ?? ""))
                    .ToList()
            );
        }
    }
}
/*
namespace BlogApp.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly UserManager<User> _userManager;

        public BlogService(IBlogRepository blogRepository, UserManager<User> userManager)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<BlogDto>> GetAllBlogsAsync()
        {
            var blogs = await _blogRepository.GetAllAsync();

            return blogs.Select(b => new BlogDto(
                b.Id,
                b.Title,
                b.Details,
                b.LastUpdateTime,
                b.ProjectOwners.Select(po =>
                    new UserDto(po.UserId, po.User.UserName ?? "", po.User.Email ?? ""))
                    .ToList()
            ));
        }

        public async Task<BlogDto?> GetByIdAsync(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null) return null;

            return new BlogDto(
                blog.Id,
                blog.Title,
                blog.Details,
                blog.LastUpdateTime,
                blog.ProjectOwners.Select(po =>
                    new UserDto(po.UserId, po.User.UserName ?? "", po.User.Email ?? ""))
                    .ToList()
            );
        }

        public async Task<BlogDto> AddBlogAsync(CreateBlogRequest request, string currentUserId)
        {
            var blog = new Blog
            {
                Title = request.Title,
                Details = request.Details,
                LastUpdateTime = DateTime.UtcNow
            };

            // Add current user as owner
            blog.ProjectOwners.Add(new ProjectOwner
            {
                UserId = currentUserId
            });

            // Add co-owners if provided
            if (request.CoOwnerIds != null)
            {
                foreach (var coOwnerId in request.CoOwnerIds.Distinct())
                {
                    if (coOwnerId != currentUserId)
                    {
                        blog.ProjectOwners.Add(new ProjectOwner { UserId = coOwnerId });
                    }
                }
            }

            await _blogRepository.AddAsync(blog);

            return await GetByIdAsync(blog.Id) ?? throw new Exception("Failed to create blog");
        }

        public async Task<BlogDto?> UpdateBlogAsync(int id, UpdateBlogRequest request, string currentUserId)
        {
            if (!await _blogRepository.IsUserOwnerAsync(id, currentUserId))
                throw new UnauthorizedAccessException("You are not an owner of this blog");

            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null) return null;

            blog.Title = request.Title;
            blog.Details = request.Details;
            blog.LastUpdateTime = DateTime.UtcNow;

            // Reset owners
            blog.ProjectOwners.Clear();

            // Add current user back as owner
            blog.ProjectOwners.Add(new ProjectOwner { BlogId = blog.Id, UserId = currentUserId });

            // Add co-owners
            if (request.CoOwnerIds != null)
            {
                foreach (var coOwnerId in request.CoOwnerIds.Distinct())
                {
                    if (coOwnerId != currentUserId)
                    {
                        blog.ProjectOwners.Add(new ProjectOwner { BlogId = blog.Id, UserId = coOwnerId });
                    }
                }
            }

            await _blogRepository.UpdateAsync(blog);

            return await GetByIdAsync(blog.Id);
        }

        public async Task<bool> DeleteBlogAsync(int id, string currentUserId)
        {
            if (!await _blogRepository.IsUserOwnerAsync(id, currentUserId))
                throw new UnauthorizedAccessException("You are not an owner of this blog");

            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null) return false;

            await _blogRepository.DeleteAsync(blog);
            return true;
        }
    }
}
*/