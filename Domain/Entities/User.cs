namespace Domain.Entities;

public class User(string username, string email, string password)
{
    public Guid Id { get; private set; } = new();
    public required Username Username { get; set; } = new(username);
    public required Email Email { get; set; } = new(email);
    public required Password Password { get; set; } = new(password);
    public List<Character>? Characters { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}