using System.Diagnostics;
using System.Text.RegularExpressions;
using BlogPlatform.API.Data;
using BlogPlatform.API.Envelopes.Requests;
using BlogPlatform.API.Envelopes.Responses;
using BlogPlatform.API.Helpers;
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
        var tags = await _databaseContext.BlogPostsTags
            .AsNoTracking()
            .Include(x => x.Tag)
            .Where(x => x.BlogPostSlug == slug)
            .Select(x => x.Tag.Name)
            .ToListAsync();
        
        var blogPost = await _databaseContext.BlogPosts
            .AsNoTracking()
            .Where(x => x.Slug == slug)
            .Select(x => new GetSingleBlogPostResponseDto
            {
                Slug = x.Slug,
                Title = x.Title,
                Description = x.Description,
                Body = x.Body,
                Tags = tags,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .FirstOrDefaultAsync();

        if (blogPost is null)
        {
            throw new NotImplementedException();
        }
        
            return blogPost;
    }

    public async Task<GetBlogPostsResponseDto> GetBlogPosts(string? tagFilter)
    {
        ICollection<GetSingleBlogPostResponseDto> blogPostsResponseList = new List<GetSingleBlogPostResponseDto>();
        if (tagFilter is null)
        {
            var blogPosts = await _databaseContext.BlogPosts
                .Include(x=>x.Tags)
                .AsNoTracking()
                .ToListAsync();
            var tags = new List<string>();

            foreach (var blogPost in blogPosts)
            {
                if (blogPost.Tags != null)
                {
                    foreach (var tag in blogPost.Tags)
                    {
                        tags.Add(tag.Name);
                    }
                }
                    
                var blogPostDto = new GetSingleBlogPostResponseDto
                {
                    Slug = blogPost.Slug,
                    Title = blogPost.Title,
                    Description = blogPost.Description,
                    Body = blogPost.Body,
                    Tags = new List<string>(tags),
                    CreatedAt = blogPost.CreatedAt,
                    UpdatedAt = blogPost.UpdatedAt
                };
                blogPostsResponseList.Add(blogPostDto);
                tags.Clear();
            }
        }
        else
        {
            var blogPosts = await _databaseContext.BlogPosts
                .Include(x => x.Tags)
                .ToListAsync();
            var tags = new List<string>();
            var flag = false;
            foreach (var blogPost in blogPosts)
            {
                if (blogPost.Tags != null)
                {
                    foreach (var tag in blogPost.Tags)
                    {
                        tags.Add(tag.Name);
                        if (tag.Name == tagFilter) flag = true;
                    } 
                }
                    
                if (!flag)
                {
                    tags.Clear();
                    continue;
                }

                flag = false;
                var blogPostDto = new GetSingleBlogPostResponseDto
                {
                    Slug = blogPost.Slug,
                    Title = blogPost.Title,
                    Description = blogPost.Description,
                    Body = blogPost.Body,
                    Tags = new List<string>(tags),
                    CreatedAt = blogPost.CreatedAt,
                    UpdatedAt = blogPost.UpdatedAt
                };
                blogPostsResponseList.Add(blogPostDto);
                tags.Clear();
            }
        }
        return new GetBlogPostsResponseDto
       {
           BlogPosts = blogPostsResponseList,
           PostsCount = blogPostsResponseList.Count
       };
    }

    public async Task<GetSingleBlogPostResponseDto> CreateBlogPost(CreateBlogPostRequestDto createBlogPostRequestDto)
    {
        if (createBlogPostRequestDto is null)
        {
            throw new NotImplementedException();
        }
        var newSlug = GenerateBlogPostSlug.GenerateSlug(createBlogPostRequestDto.Title);
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
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
        await _databaseContext.BlogPosts.AddAsync(blogPost);
        await _databaseContext.SaveChangesAsync();

        var tagsExisting = await _databaseContext.Tags
            .AsNoTracking()
            .Select(x => x.Name)
            .ToListAsync();
        if (createBlogPostRequestDto.Tags != null)
        {
            foreach (var tagName in createBlogPostRequestDto.Tags)
            {
                if (!tagsExisting.Contains(tagName))
                {
                    Tag tag = new Tag
                    {
                        Name = tagName,
                    };
                    await _databaseContext.Tags.AddAsync(tag);
                    await _databaseContext.SaveChangesAsync();

                    await _databaseContext.BlogPostsTags.AddAsync(new BlogPostTag
                    {
                        BlogPostSlug = newSlug,
                        TagId = tag.Id,
                    });
                }
                else
                {
                    var tagId = await _databaseContext.Tags
                        .AsNoTracking()
                        .Where(x => x.Name == tagName)
                        .Select(x => x.Id)
                        .FirstAsync();
                    await _databaseContext.BlogPostsTags.AddAsync(new BlogPostTag
                    {
                        BlogPostSlug = newSlug,
                        TagId = tagId,
                    });
                }
            }
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
}