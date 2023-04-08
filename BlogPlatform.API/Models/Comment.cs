namespace BlogPlatform.API.Models;

public class Comment
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Body { get; set; }
    public BlogPost BlogPost { get; set; }
    public string BlogPostSlug { get; set; }
}