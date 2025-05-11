using System.Numerics;
using Domain.Entities.Stats;

namespace Domain.Entities;

public class Item
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string? Description { get; private set; }
    public StatsCollection? Stats { get; private set; } = new StatsCollection();
    public virtual bool IsStackable { get; set; } = false;
    public virtual int Amount { get; private set; } = 1;
    public Vector2 Size { get; set; } = Vector2.One;

    public Item(string name, string description, StatsCollection stats)
    {
        Name = name;
        Description = description;
        Stats = stats;
    }

    public Item(string name, string description)
    {
        Name = name;
        Description = description;
    }
}