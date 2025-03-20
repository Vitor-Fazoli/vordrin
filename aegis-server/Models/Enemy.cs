using System.ComponentModel.DataAnnotations;

namespace aegis_server.Models;

public class Enemy
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "Goblin";
    public int Hp { get; set; } = 100;
    public int MaxHp { get; set; } = 100;
    public Guid? SessionId { get; set; }
}