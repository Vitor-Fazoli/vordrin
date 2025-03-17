namespace Aegis.Entities;

public class Player
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Weapon? Weapon { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Armor { get; set; }
    public int MaxArmor { get; set; }
}