using BlogPlatform.API.Envelopes.Responses;
using BlogPlatform.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.API.Controllers;

[Route("api/posts")]
[ApiController]
public class BlogPostController:ControllerBase
{
    private readonly IBlogPostService _blogPostService;

    public BlogPostController(IBlogPostService blogPostService)
    {
        _blogPostService = blogPostService;
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<GetSingleBlogPostResponseDto>> GetSingleBlogPost(string slug)
    {
        return Ok(await _blogPostService.GetBlogPostBySlug(slug));
    }

    [HttpGet]
    public async Task<ActionResult<GetBlogPostsResponseDto>> GetBlogPosts([FromQuery] string? tag)
    {
        return Ok(await _blogPostService.GetBlogPosts(tag));
    }
}