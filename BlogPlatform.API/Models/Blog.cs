using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.API.Models;

public class Blog
{
    [Key]
    public string Slug { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Body { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}