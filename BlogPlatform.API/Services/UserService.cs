using BlogPlatform.API.Data;
using BlogPlatform.API.Envelopes.Requests;
using BlogPlatform.API.Envelopes.Responses;
using BlogPlatform.API.Models;
using BlogPlatform.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.API.Services;

public class UserService:IUserService
{
    private readonly DatabaseContext _databaseContext;
    private readonly UserManager<User> _userManager;

    public UserService(DatabaseContext databaseContext, UserManager<User> userManager)
    {
        _databaseContext = databaseContext;
        _userManager = userManager;
    }
    
    public async Task<GetSingleUserResponseDto> CreateUser(CreateUserRequestDto createUserRequestDto)
    {
        var result = await _userManager.CreateAsync(
            new User
            {
                FirstName = createUserRequestDto.FirstName,
                LastName = createUserRequestDto.LastName,
                UserName = createUserRequestDto.UserName,
                Email = createUserRequestDto.Email,
                
            }, createUserRequestDto.Password);

        return new GetSingleUserResponseDto
        {
            FirstName = createUserRequestDto.FirstName,
            LastName = createUserRequestDto.LastName,
            UserName = createUserRequestDto.UserName,
            Email = createUserRequestDto.Email
        };
    }
}