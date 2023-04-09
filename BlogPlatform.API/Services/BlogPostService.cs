using BlogPlatform.API.Data;
using BlogPlatform.API.Envelopes.Responses;
using BlogPlatform.API.Models;
using BlogPlatform.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.API.Services;

public class BlogPostService:IBlogPostService
{
    private readonly DatabaseContext _databaseContext;

    public BlogPostService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    public async Task<GetSingleBlogPostResponseDto> GetBlogPostBySlug(string slug)
    {
        var blogPost = await _databaseContext.BlogPosts
            .AsNoTracking()
            .Where(x => x.Slug == slug)
            .Select(x => new GetSingleBlogPostResponseDto
            {
                Slug = x.Slug,
                Title = x.Title,
                Description = x.Description,
                Body = x.Body,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .FirstOrDefaultAsync();
        
        if (blogPost is null) throw new NotImplementedException();
        
            return blogPost;
    }

    public async Task<GetBlogPostsResponseDto> GetBlogPosts(string? tagName)
    {
        ICollection<BlogPost> blogPosts;
        ICollection<GetSingleBlogPostResponseDto> blogPostsReturn = null;
        var count = 0;
        if (tagName is null)
        {
            blogPosts = await _databaseContext.BlogPosts.ToListAsync();
        }
        else
        {
            var tag = _databaseContext.Tags.Where(x => x.Name == tagName);
            blogPosts = await _databaseContext.BlogPosts.Where(x => x.Tags == tag).ToListAsync();
        }
        
        return new GetBlogPostsResponseDto
        {
            BlogPosts = blogPostsReturn,
            PostsCount = count
        };
        //    throw new NotImplementedException();
    }
}