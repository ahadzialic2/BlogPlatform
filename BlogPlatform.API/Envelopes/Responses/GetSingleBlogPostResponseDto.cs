using BlogPlatform.API.Models;

namespace BlogPlatform.API.Envelopes.Responses;

public class GetSingleBlogPostResponseDto
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Body { get; set; }
    public ICollection<string>? Tags { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}