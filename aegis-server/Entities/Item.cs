namespace aegis_server.Entities;

public class Item {
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float DamagePerClick {get; set;}
    public Type ItemType {get; set;}
}