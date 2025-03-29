using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Email(string email)
{
    [EmailAddress]
    private readonly string _email = email;
    public string GetEmail()
    {
        return _email;
    }
}