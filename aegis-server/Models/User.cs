using System.ComponentModel.DataAnnotations;

namespace aegis_server.Models;

public class User
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString(); // GUID como ID

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? PasswordHash { get; set; }
}
