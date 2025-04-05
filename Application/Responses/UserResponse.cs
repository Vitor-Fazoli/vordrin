using Infrastructure.Dtos;

namespace Application.Responses;

public class UserResponse(UserDto user)
{
    public string Id { get; set; } = user.Id.ToString();
    public string Username { get; set; } = user.Username;
    public string Email { get; set; } = user.Email;
    public DateTime CreatedAt { get; set; } = user.CreatedAt;

    public List<>
}