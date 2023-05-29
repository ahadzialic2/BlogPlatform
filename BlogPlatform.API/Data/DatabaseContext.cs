using BlogPlatform.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogPlatform.API.Data;

public class DatabaseContext:IdentityUserContext<User, Guid>
{
    protected readonly IConfiguration Configuration;
    public DatabaseContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(Configuration.GetConnectionString("LocalConnection"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BlogPost>()
            .HasMany(e => e.Tags)
            .WithMany(e => e.BlogPosts)
            .UsingEntity<BlogPostTag>();
    }
    
    public DbSet<BlogPost> BlogPosts { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Tag> Tags { get; set; }
    public DbSet<BlogPostTag> BlogPostsTags { get; set; }
}