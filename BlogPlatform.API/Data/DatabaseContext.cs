using BlogPlatform.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.API.Data;

public class DatabaseContext:DbContext
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
    
    public DbSet<BlogPost> BlogPosts { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Tag> Tags { get; set; }
}