using Domain.Interfaces;

namespace Domain.Entities.Attributes.PrimaryAttributes;

public class Wisdom(int wisdom) : IAttribute<int>
{
    private int _wisdom = wisdom;

    public int Get()
    {
        return _wisdom;
    }

    public void Set(int wisdom)
    {
        if (wisdom < 1)
            _wisdom = 1;

        // This is necessary because wisdom is summed with other attributes to total 15;
        if (wisdom > 11)
            _wisdom = 11;

        _wisdom = wisdom;
    }
}