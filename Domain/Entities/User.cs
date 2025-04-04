namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = new();
    public Username Username { get; set; }
    public Email Email { get; set; }
    public Password Password { get; set; }
    public List<Character> Characters { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public User(string username, string email, string password)
    {
        Username = new(username);
        Email = new(email);
        Password = new(password);
    }

    public User(Guid id, Username username, Email email, Password password, List<Character> characters, DateTime createdAt)
    {
        Id = id;
        Username = username;
        Email = email;
        Password = password;
        Characters = characters;
        CreatedAt = createdAt;
    }
}