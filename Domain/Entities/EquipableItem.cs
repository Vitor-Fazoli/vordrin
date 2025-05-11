using Domain.Entities;
using Domain.Entities.Stats;
using Domain.Enums;

namespace Domain.Entities;

public class EquipableItem(string name, string description, EquipmentSlot slot, int requiredLevel) : Item(name, description)
{
    public EquipmentSlot Slot { get; private set; } = slot;
    public int RequiredLevel { get; private set; } = requiredLevel;
    public override bool IsStackable { get; set; } = false;
}