namespace Domain.Entities;

public class Item(int id, string name)
{
    public long Id { get; private set; } = id;
    public string? Name { get; private set; } = name;
    public int Width { get; set; } = 1;
    public int Height { get; set; } = 1;

    public virtual void UseItem(Character character)
    {
        
    }
}