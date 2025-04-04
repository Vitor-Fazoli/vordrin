using System.ComponentModel;

namespace Application.Dtos;

public class UserAuthDto
{
    public required string Username { get; set; }

    [PasswordPropertyText]
    public required string Password { get; set; }
}