using Infrastructure.Dtos;

namespace Application.Requests;

public class UserRequest
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    /// <summary>
    /// Parses the UserRequest to a UserDto.
    /// This is used to convert the request data into a format that can be used by the application.
    /// </summary>
    /// <returns>UserDTO with values of input</returns>
    public UserDto Parse()
    {
        return new UserDto()
        {
            Username = Username,
            Email = Email,
            Password = Password
        };
    }
}