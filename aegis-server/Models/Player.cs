namespace aegis_server.Models;

public class Player(string id)
{
    public string SessionId { get; set; } = id;
    public string Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; } = 1;
    public Guid? Weapon { get; set; }
    public int Health { get; set; } = 100;
    public int HealthMax { get; set; } = 100;
    public bool IsInvulnerable { get; set; } = false;
}