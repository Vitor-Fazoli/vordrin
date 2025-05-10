namespace Domain.Entities;
public class Stat(string name, float baseValue)
{
    public string Name { get; private set; } = name;
    public float BaseValue { get; private set; } = baseValue;

    private float _flatBonus = 0;
    private float _percentageBonus = 0;

    public float FlatBonus => _flatBonus;
    public float PercentageBonus => _percentageBonus;

    // Valor total calculado com todos os modificadores
    public float TotalValue => CalculateTotalValue();

    private float CalculateTotalValue()
    {
        // Fórmula: (BaseValue + FlatBonus) * (1 + PercentageBonus)
        return (BaseValue + _flatBonus) * (1 + _percentageBonus);
    }

    // Métodos para manipulação de modificadores
    public void SetBaseValue(float value)
    {
        BaseValue = value;
    }

    public void AddFlatBonus(float value)
    {
        _flatBonus += value;
    }

    public void RemoveFlatBonus(float value)
    {
        _flatBonus -= value;
    }

    public void AddPercentageBonus(float value)
    {
        _percentageBonus += value;
    }

    public void RemovePercentageBonus(float value)
    {
        _percentageBonus -= value;
    }

    public void ResetBonuses()
    {
        _flatBonus = 0;
        _percentageBonus = 0;
    }
}