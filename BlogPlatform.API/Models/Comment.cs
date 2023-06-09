namespace BlogPlatform.API.Models;

public class Comment
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Body { get; set; } = string.Empty;
    public BlogPost BlogPost { get; set; } = null!;
    public string BlogPostSlug { get; set; } = null!;
}