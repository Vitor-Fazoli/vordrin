namespace Application.Responses;

public class LoginResponse
{
    public required string Token { get; set; }
    public DateTime ExpiredAt { get; set; }
    public required string Username { get; set; }
}