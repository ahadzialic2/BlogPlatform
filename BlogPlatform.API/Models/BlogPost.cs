using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.API.Models;

public class BlogPost
{
    [Key] public string Slug { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Body { get; set; }
    public ICollection<Tag>? Tags { get; } = new List<Tag>();
    public ICollection<BlogPostTag> BlogPostTags { get; } = new List<BlogPostTag>();
    public ICollection<Comment>? Comments { get; } = new List<Comment>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}