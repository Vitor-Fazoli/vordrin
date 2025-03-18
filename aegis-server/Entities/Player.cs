namespace aegis_server.Entities;

public class Player {
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Armor {get; set;}
}