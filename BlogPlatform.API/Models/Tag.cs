using System.Diagnostics.CodeAnalysis;

namespace BlogPlatform.API.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
}