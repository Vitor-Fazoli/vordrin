namespace Infrastructure.Security;

public static class Crypt
{
    public static string HashPassword(string str)
    {
        return BCrypt.Net.BCrypt.HashPassword(str);
    }

    public static bool VerifyPassword(string str, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(str, hash);
    }
}