namespace Infrastructure.Security;

public class TokenSettings
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int HoursUntilExpired { get; set; }
}