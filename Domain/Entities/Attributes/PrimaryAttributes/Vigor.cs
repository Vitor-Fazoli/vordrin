using Domain.Interfaces;

namespace Domain.Entities.Attributes.PrimaryAttributes;

public class Vigor(int vigor) : IAttribute<int>
{
    private int _vigor = vigor;

    public int Get()
    {
        return _vigor;
    }

    public void Set(int vigor)
    {
        if (vigor < 1)
            _vigor = 1;

        // This is necessary because vigor is summed with other attributes to total 15;
        if (vigor > 11)
            _vigor = 11;

        _vigor = vigor;
    }
}