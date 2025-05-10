
using Domain.Entities.Attributes;
using Domain.Enums;
using Domain.Interfaces;
using Vordrin.Domain.Entities;

namespace Domain.Entities;

public class Enemy : Alived
{
    public string Type { get; private set; }
    public int ExperienceReward { get; private set; }
    public Enemy(string name, string type, int level)
        : base(name, level)
    {
        Type = type;
    }
}