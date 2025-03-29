namespace Infrastructure.Security;

public static class Crypt
{
    public static string HashPassword(string str)
    {
        return BCrypt.Net.BCrypt.HashPassword(str);
    }
}