namespace BlogPlatform.API.Models;

public class BlogPostTag
{
    public string BlogPostSlug { get; set; } = string.Empty;
    public int TagId { get; set; }
}