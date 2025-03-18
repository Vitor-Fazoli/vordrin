namespace aegis_server.Entities;

public class Bag {
    public Guid Id { get; set; }
    public Player? Player { get; set; } = null!;
    public List<Item> Items { get; set; } = new List<Item>();
    public int MaxSize { get; set; }
    public int CurrentSize { get; set; }
}