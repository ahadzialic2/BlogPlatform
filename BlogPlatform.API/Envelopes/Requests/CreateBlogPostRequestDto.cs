namespace BlogPlatform.API.Envelopes.Requests;

public class CreateBlogPostRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Body { get; set; }
    public ICollection<string>? Tags { get; set; }
}