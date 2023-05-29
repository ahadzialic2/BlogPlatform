using BlogPlatform.API.Envelopes.Requests;
using BlogPlatform.API.Envelopes.Responses;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.API.Services.Interfaces;

public interface IUserService
{
    Task<GetSingleUserResponseDto> CreateUser(CreateUserRequestDto createUserRequestDto);
} 