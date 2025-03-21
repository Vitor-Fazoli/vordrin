namespace aegis_server.Models;

public class Player(string id, string name)
{
    public string Id { get; set; } = id;
    public string Name { get; set; } = name;
    public int Level { get; set; } = 1;
    public Guid? Weapon { get; set; }
    public int Hp { get; set; } = 100;
    public int MaxHp { get; set; } = 100;
}