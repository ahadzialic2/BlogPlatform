using BlogPlatform.API.Envelopes.Requests;
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
        return Ok(await _blogPostService.GetSingleBlogPost(slug));
    }

    [HttpGet]
    public async Task<ActionResult<GetBlogPostsResponseDto>> GetBlogPosts([FromQuery] string? tag)
    {
        return Ok(await _blogPostService.GetBlogPosts(tag));
    }

    [HttpPost]
    public async Task<ActionResult<GetSingleBlogPostResponseDto>> CreateBlogPost(
        [FromBody]CreateBlogPostRequestDto createBlogPostRequestDto)
    {
        try
        {
            return Ok(await _blogPostService.CreateBlogPost(createBlogPostRequestDto));
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status406NotAcceptable, ex.Message);
        }

    }
}