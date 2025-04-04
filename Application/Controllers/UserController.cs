using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Services;
using Infrastructure.Config;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(VordrinDbContext context) : ControllerBase
{
    private readonly VordrinDbContext _context = context;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] UserDto user)
    {
        try
        {
            UserService userService = new(_context);
            await userService.Create(user);

            return Ok("User registered!");
        }
        catch (ExternalException)
        {
            return BadRequest();
        }
    }

    [HttpPost("auth")]
    public async Task<IActionResult> Auth([FromBody] UserAuthDto auth)
    {
        try
        {
            UserService userService = new(_context);
            //await userService.Create(user);

            return Ok("User registered!");
        }
        catch (ExternalException)
        {
            return BadRequest();
        }
    }
}