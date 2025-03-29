using Domain.Interfaces;

namespace Domain.Entities.Attributes;

public class Aggro : IAttribute<int>
{
    private int _aggro;
    public int Get()
    {
        return _aggro;
    }

    public void Set(int aggro)
    {
        if (aggro >= 1000)
            _aggro = 1000;

        if (aggro <= 0)
            _aggro = 0;

        _aggro = aggro;
    }

    public void Increase(int amount)
    {
        Set(Get() + amount);
    }

    public void Decrease(int amount)
    {
        Set(Get() - amount);
    }
}