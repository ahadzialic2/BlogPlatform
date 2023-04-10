using BlogPlatform.API.Envelopes.Requests;
using BlogPlatform.API.Envelopes.Responses;
using BlogPlatform.API.Models;

namespace BlogPlatform.API.Services.Interfaces;

public interface IBlogPostService
{
    Task<GetSingleBlogPostResponseDto> GetSingleBlogPost(string slug);
    Task<GetBlogPostsResponseDto> GetBlogPosts(string? tag);
    Task<GetSingleBlogPostResponseDto> CreateBlogPost(CreateBlogPostRequestDto createBlogPostRequestDto);
}