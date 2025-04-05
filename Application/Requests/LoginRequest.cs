using System.ComponentModel;

namespace Application.Requests;

public class LoginRequest
{
    /// <summary>
    /// Email or username of the user.
    /// </summary>
    public required string Credential { get; set; }

    [PasswordPropertyText]
    public required string Password { get; set; }

    public bool Validate()
    {
        return !string.IsNullOrWhiteSpace(Credential) && !string.IsNullOrWhiteSpace(Password);
    }
}