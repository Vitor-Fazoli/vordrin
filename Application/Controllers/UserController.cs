using System.Runtime.InteropServices;
using System.Security.Claims;
using Application.Interfaces;
using Application.Requests;
using Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserRequest user)
    {
        try
        {
            await _userService.Create(user);

            return Ok("User registered!");
        }
        catch (ExternalException)
        {
            return BadRequest();
        }
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUserAuth()
    {
        var user = User;

        if (user is null)
        {
            return Unauthorized("User not authenticated!");
        }

        if (user.Identity is null || !user.Identity.IsAuthenticated)
        {
            return Unauthorized("User not authenticated!");
        }

        string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        try
        {
            UserResponse userAuth = await _userService.GetUserById(Guid.Parse(userId));

            

            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("User not found!");
        }
    }
}