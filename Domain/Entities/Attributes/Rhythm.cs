using Domain.Interfaces;

namespace Domain.Entities.Attributes;

public class Rhythm(int rhythm) : IAttribute<int>
{
    private int _rhythm = rhythm;

    public int Get()
    {
        return _rhythm;
    }

    public void Set(int rhythm)
    {
        if (rhythm < 1)
            _rhythm = 1;

        // This is necessary because rhythm is summed with other attributes to total 15;
        if (rhythm > 11)
            _rhythm = 11;

        _rhythm = rhythm;
    }
}