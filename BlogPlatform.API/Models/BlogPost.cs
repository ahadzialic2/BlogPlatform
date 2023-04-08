using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.API.Models;

public class BlogPost
{
    [Key] public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Body { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}