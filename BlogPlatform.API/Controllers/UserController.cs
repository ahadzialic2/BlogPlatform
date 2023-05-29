using BlogPlatform.API.Envelopes.Requests;
using BlogPlatform.API.Envelopes.Responses;
using BlogPlatform.API.Services;
using BlogPlatform.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.API.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    //private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<GetSingleUserResponseDto>> CreateUser(CreateUserRequestDto createUserRequestDto)
    {
        return Created("", await _userService.CreateUser(createUserRequestDto));
    }
}