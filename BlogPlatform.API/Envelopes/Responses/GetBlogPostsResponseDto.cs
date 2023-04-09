namespace BlogPlatform.API.Envelopes.Responses;

public class GetBlogPostsResponseDto
{
    public ICollection<GetSingleBlogPostResponseDto> BlogPosts { get; set; }
    public int PostsCount { get; set; }
}