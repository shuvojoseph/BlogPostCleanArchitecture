using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>//DbContext//IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<ProjectOwner> ProjectOwners { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ProjectOwner>()
            .HasKey(po => new { po.BlogId, po.UserId });

        builder.Entity<ProjectOwner>()
            .HasOne(po => po.Blog)
            .WithMany(b => b.ProjectOwners)
            .HasForeignKey(po => po.BlogId);

        builder.Entity<ProjectOwner>()
            .HasOne(po => po.User)
            .WithMany(u => u.ProjectOwners)
            .HasForeignKey(po => po.UserId);
    }
}