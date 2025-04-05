using System.ComponentModel;

namespace Application.Dtos;

public class LoginRequest
{
    /// <summary>
    /// Email or username of the user.
    /// </summary>
    public required string Credential { get; set; }

    [PasswordPropertyText]
    public required string Password { get; set; }
}