namespace Domain.Entities;

public class Item(string id, string name)
{
    public string Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string? Description { get; private set; }
    public int Width { get; set; } = 1;
    public int Height { get; set; } = 1;
}