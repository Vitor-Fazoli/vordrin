using Domain.Interfaces;

namespace Domain.Entities.Attributes.PrimaryAttributes;

public class Ferocity(int ferocity) : IAttribute<int>
{
    private int _ferocity = ferocity;

    public int Get()
    {
        return _ferocity;
    }

    public void Set(int ferocity)
    {
        if (ferocity < 1)
            _ferocity = 1;

        // This is necessary because ferocity its summed with others attributes to total 15;
        if (ferocity > 11)
            _ferocity = 11;

        _ferocity = ferocity;
    }
}