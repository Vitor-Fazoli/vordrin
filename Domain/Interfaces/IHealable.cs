using Domain.Entities.Attributes;

namespace Domain.Interfaces;

public interface IHealable
{
    public void ReceiveHeal(Heal heal);
}