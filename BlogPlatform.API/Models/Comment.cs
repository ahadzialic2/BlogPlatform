namespace BlogPlatform.API.Models;

public class Comment
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Body { get; set; }
    public Blog Blog { get; set; }
    public int BlogId { get; set; }
}