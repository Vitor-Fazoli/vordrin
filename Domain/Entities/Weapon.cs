using Domain.Entities.Attributes;
using Domain.Enums;

namespace Domain.Entities;
public class Weapon(string name, Damage damage, WeaponType weaponClass) : Item(0, name)
{
    public WeaponType Class { get; set; } = weaponClass;
    public Damage Damage { get; set; } = damage;
    public int Value { get; set; } = 2;
}