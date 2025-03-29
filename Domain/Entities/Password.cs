namespace Domain.Entities;

public class Password
{
    public string StatPassword { get; set; }

    private static string Validate(string password)
    {
        if (password.Length <= 12)
            throw new ArgumentException("password is too much short");

        return password;
    }

    public Password() { }

    public Password(string password)
    {
        StatPassword = Validate(password);
    }

    public string GetPassword()
    {
        return StatPassword;
    }
}