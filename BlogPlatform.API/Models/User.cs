using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.API.Models;

public class User:IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}