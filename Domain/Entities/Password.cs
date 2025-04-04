namespace Domain.Entities;

public class Password(string password)
{
    private readonly string _password = Validate(password);

    private static string Validate(string password)
    {
        if (password.Length <= 12)
            throw new ArgumentException("password is too much short");

        return password;
    }

    public string Get()
    {
        return _password;
    }
}