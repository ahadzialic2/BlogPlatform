namespace BlogPlatform.API.Envelopes.Responses;

public class GetBlogPostsResponseDto
{
    public ICollection<GetSingleBlogPostResponseDto> BlogPosts { get; set; } = new List<GetSingleBlogPostResponseDto>();
    public int PostsCount { get; set; }
}