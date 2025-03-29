using Domain.Constants;
using Domain.Entities;

namespace Infrastructure.Memory;

public static class Weapons
{
    public static readonly Dictionary<int, Weapon> DictonaryWeapons = new(){
        {1, new Weapon("Rusted Greatsword", new(1.5f, 0.2f, 1.3f), WeaponClass.Greatsword) },

        {2, new("Hunter Bow", new(1.8f, 0.2f, 1.3f), WeaponClass.Bow)},

        {3, new("Medical Syringe", new(0.8f, 0.2f, 1.3f), WeaponClass.Syringe)}
    };
}
