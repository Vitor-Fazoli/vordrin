using System.ComponentModel.DataAnnotations;

namespace aegis_server.Models;

public class User
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString(); // GUID como ID

    [Required]
    [MinLength(5)]
    [MaxLength(20)]
    public string? Username { get; set; }

    [Required]
    [MinLength(8)]
    public string? PasswordHash { get; set; }
}
