namespace Domain.Entities;

public class Username(string username)
{
    public string StatUsername { get; set; } = username;

    public string Get()
    {
        return StatUsername;
    }
}