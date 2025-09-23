// BlogApp.Application/DTOs/BlogDtos.cs
public record BlogDto(int Id, string Title, string Details, DateTime LastUpdateTime, List<UserDto> Owners);
public record UserDto(string Id, string Name, string Email);
public record CreateBlogRequest(string Title, string Details, List<string> CoOwnerIds);
public record UpdateBlogRequest(string Title, string Details, List<string> CoOwnerIds);