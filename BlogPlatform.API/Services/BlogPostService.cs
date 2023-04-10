using System.Text.RegularExpressions;
using BlogPlatform.API.Data;
using BlogPlatform.API.Envelopes.Requests;
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
    public async Task<GetSingleBlogPostResponseDto> GetSingleBlogPost(string slug)
    {
        var tgs = await _databaseContext.BlogPostsTags
            .Where(x => x.BlogPostSlug == slug)
            .Select(x=> x.BlogPostSlug)
            .ToListAsync();

        /*
        var tagsBlogPost = _databaseContext.Tags
            .Where(x => x.BlogPosts.Any(x=> x.Slug == slug))
            .Select(x => x.Name)
            .AsQueryable();
        var tags = new List<string>();
        foreach (var tagName in tagsBlogPost)
        {
            tags.Add(tagName);
        }
        */
        var blogPost = await _databaseContext.BlogPosts
            .AsNoTracking()
            .Include(x => x.Tags)
            .Where(x => x.Slug == slug)
            .Select(x => new GetSingleBlogPostResponseDto
            {
                Slug = x.Slug,
                Title = x.Title,
                Description = x.Description,
                Body = x.Body,
                Tags = tgs,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .FirstOrDefaultAsync();
        
        if (blogPost is null) throw new NotImplementedException();
        
            return blogPost;
    }

    public async Task<GetBlogPostsResponseDto> GetBlogPosts(string? tagFilter)
    {
        ICollection<GetSingleBlogPostResponseDto> blogPostsResponseDto = new List<GetSingleBlogPostResponseDto>();
        var count = 0;
        var tags = new List<string>();

        var blogPosts = await _databaseContext.BlogPosts
            .ToListAsync();
        
        if (tagFilter is null)
        {
            foreach (var blogPost in blogPosts)
            {
                var tagsBlogPost = _databaseContext.Tags
                    .Where(x => x.BlogPosts.Any(x => x.Slug == blogPost.Slug))
                    .Select(x => x.Name)
                    .AsQueryable();
                tags = new List<string>();
                foreach (var tagName in tagsBlogPost)
                {
                    tags.Add(tagName);
                }
            }

            foreach (var blogPost in blogPosts)
            {
                blogPostsResponseDto.Add(new GetSingleBlogPostResponseDto
                {
                    Slug = blogPost.Slug,
                    Title = blogPost.Title,
                    Description = blogPost.Description,
                    Body = blogPost.Body,
                    Tags = tags,
                    CreatedAt = blogPost.CreatedAt,
                    UpdatedAt = blogPost.UpdatedAt
                });
            }
        }
        else
        {
            foreach (var blogPost in blogPosts)
            {
                var tagsBlogPost = _databaseContext.Tags
                    .Where(x => x.BlogPosts.Any(x => x.Slug == blogPost.Slug))
                    .Select(x => x.Name)
                    .Where(x => x == tagFilter)
                    .AsQueryable();
                tags = new List<string>();
                foreach (var tagName in tagsBlogPost)
                {
                    tags.Add(tagName);
                }
            }

            foreach (var blogPost in blogPosts)
            {
                blogPostsResponseDto.Add(new GetSingleBlogPostResponseDto
                {
                    Slug = blogPost.Slug,
                    Title = blogPost.Title,
                    Description = blogPost.Description,
                    Body = blogPost.Body,
                    Tags = tags,
                    CreatedAt = blogPost.CreatedAt,
                    UpdatedAt = blogPost.UpdatedAt
                });
            }
        }

        count = blogPostsResponseDto.Count();
        return new GetBlogPostsResponseDto
        {
            BlogPosts = blogPostsResponseDto,
            PostsCount = count
        };
    }

    public async Task<GetSingleBlogPostResponseDto> CreateBlogPost(CreateBlogPostRequestDto createBlogPostRequestDto)
    {
        if (createBlogPostRequestDto is null)
        {
            throw new NotImplementedException();
        }
        var newSlug = GenerateSlug(createBlogPostRequestDto.Title);
        if (newSlug.Equals(string.Empty))
        {
            throw new NotImplementedException();
        }
        
        var blogPost = new BlogPost
        {
            Slug = newSlug,
            Title = createBlogPostRequestDto.Title,
            Description = createBlogPostRequestDto.Description,
            Body = createBlogPostRequestDto.Body,
            Tags = null,
            Comments = null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
        await _databaseContext.BlogPosts.AddAsync(blogPost);
        await _databaseContext.SaveChangesAsync();

        var tgs = await _databaseContext.Tags.Select(x => x.Name).ToListAsync();
        foreach (var tagName in createBlogPostRequestDto.Tags)
        {
            if (!tgs.Contains(tagName))
            {
                await _databaseContext.Tags.AddAsync(new Tag
                {
                    Name = tagName,
                });
                await _databaseContext.SaveChangesAsync();
            }
        }
        
        foreach (var tagName in createBlogPostRequestDto.Tags)
        {
            var tagId = await _databaseContext.Tags
                .Where(x => x.Name == tagName)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
            await _databaseContext.BlogPostsTags.AddAsync(new BlogPostTag
            {
                BlogPostSlug = newSlug,
                TagId = tagId
            });
        }

        await _databaseContext.SaveChangesAsync();
        return new GetSingleBlogPostResponseDto
        {
            Slug = newSlug,
            Title = blogPost.Title,
            Description = blogPost.Description,
            Body = blogPost.Body,
            Tags = createBlogPostRequestDto.Tags,
            CreatedAt = blogPost.CreatedAt,
            UpdatedAt = blogPost.UpdatedAt
        };
    }
    public static string GenerateSlug(string phrase) 
    { 
        string str = phrase.ToLower(); 
        // invalid chars           
        str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); 
        // convert multiple spaces into one space   
        str = Regex.Replace(str, @"\s+", " ").Trim(); 
        // cut and trim 
        str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();   
        str = Regex.Replace(str, @"\s", "-"); // hyphens   
        return str; 
    }
}