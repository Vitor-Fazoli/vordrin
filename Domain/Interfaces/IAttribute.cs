namespace Domain.Interfaces;

public interface IAttribute<T>
{
    public T Get();
    public void Set(T attribute);
}