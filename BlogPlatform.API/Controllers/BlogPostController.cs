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
        try
        {
            return Ok(await _blogPostService.GetSingleBlogPost(slug));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<GetBlogPostsResponseDto>> GetBlogPosts([FromQuery] string? tag)
    {
        try
        {
            return Ok(await _blogPostService.GetBlogPosts(tag));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
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