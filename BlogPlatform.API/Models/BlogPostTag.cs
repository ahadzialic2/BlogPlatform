namespace BlogPlatform.API.Models;

public class BlogPostTag
{
    public string BlogPostSlug { get; set; } = null!;
    public int TagId { get; set; }
    public BlogPost BlogPost { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}