namespace BlogPlatform.API.Models;

public class Comment
{
    public int CommentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Body { get; set; }
    public string BlogSlug { get; set; }
    public Blog Blog { get; set; }
}