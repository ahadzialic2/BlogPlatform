using BlogPlatform.API.Envelopes.Responses;
using BlogPlatform.API.Models;

namespace BlogPlatform.API.Services.Interfaces;

public interface IBlogPostService
{
    Task<GetSingleBlogPostResponseDto> GetBlogPostBySlug(string slug);
    Task<GetBlogPostsResponseDto> GetBlogPosts(string? tag);
}