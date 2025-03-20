using System.ComponentModel.DataAnnotations;

namespace aegis_server.Models;

public class Player
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public int Level { get; set; } = 1;
    public string Weapon { get; set; } = "Greatsword";
    public int Hp { get; set; } = 100;
    public Guid? SessionId { get; set; }
}
