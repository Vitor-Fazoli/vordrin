using Domain.Enums;

namespace Domain.Interfaces;

public interface IStat<T>
{
    public T Get();
    public StatType Type { get; }
    public void Set(T stat);
}