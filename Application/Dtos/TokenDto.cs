namespace Application.Dtos;

public class TokenDto
{
    public string token;

    public DateTime ExpiredAt = DateTime.Now.AddDays(1);
}