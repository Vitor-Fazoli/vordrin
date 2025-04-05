namespace Domain.Entities.Attributes.PrimaryAttributes;

public class PrimaryAttributes
{
    public Ferocity Ferocity { get; private set; }
    public Precision Precision { get; private set; }
    public Rhythm Rhythm { get; private set; }
    public Vigor Vigor { get; private set; }
    public Wisdom Wisdom { get; private set; }

    public PrimaryAttributes(Ferocity ferocity, Precision precision, Rhythm rhythm, Vigor vigor, Wisdom wisdom)
    {
        if (ferocity.Get() + precision.Get() + rhythm.Get() + vigor.Get() + wisdom.Get() != 15)
            throw new ArgumentException("The sum of all attributes must equal 15.");

        if (ferocity.Get() < 1 || precision.Get() < 1 || rhythm.Get() < 1 || vigor.Get() < 1 || wisdom.Get() < 1)
            throw new ArgumentException("All attributes must be at least 1.");

        Ferocity = ferocity;
        Precision = precision;
        Rhythm = rhythm;
        Vigor = vigor;
        Wisdom = wisdom;
    }

    public bool AreValid()
    {
        // Ensure all attributes are at least 1
        if (Ferocity.Get() < 1 || Precision.Get() < 1 || Rhythm.Get() < 1 || Vigor.Get() < 1 || Wisdom.Get() < 1)
            return false;

        // Ensure the total of all attributes equals 15
        int total = Ferocity.Get() + Precision.Get() + Rhythm.Get() + Vigor.Get() + Wisdom.Get();
        return total == 15;
    }
}