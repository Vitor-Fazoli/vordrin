using Domain.Entities.Buffs;
using Domain.Entities.Stats;
using Domain.Enums;

namespace Vordrin.Domain.Entities.Buffs;

public class BuffManager(StatsCollection stats)
{
    private readonly List<Buff> _buffs = [];
    private readonly StatsCollection _stats = stats ?? throw new ArgumentNullException(nameof(stats));

    public IReadOnlyList<Buff> ActiveBuffs => [.. _buffs.Where(b => b.IsActive)];

    public void AddBuff(Buff buff)
    {
        var existingBuff = _buffs.FirstOrDefault(b =>
            b.GetType() == buff.GetType() &&
            b.Name == buff.Name &&
            b.IsActive);

        if (existingBuff != null)
        {
            // Se o buff já existe e é stackable, adiciona um stack
            if (existingBuff is StackableBuff stackableBuff)
            {
                if (stackableBuff.AddStack())
                {
                    // Aplica o efeito adicional
                    buff.Apply(_stats);
                }

                // Reseta duração
                existingBuff.Refresh();
            }
            else
            {
                // Se não é stackable, apenas reseta a duração
                existingBuff.Refresh();
            }
        }
        else
        {
            // Adiciona novo buff e aplica efeito
            _buffs.Add(buff);
            buff.Apply(_stats);
        }
    }

    public void RemoveBuff(Guid buffId)
    {
        var buff = _buffs.FirstOrDefault(b => b.Id == buffId);
        if (buff != null)
        {
            buff.Remove(_stats);
            _buffs.Remove(buff);
        }
    }

    public void ClearExpiredBuffs()
    {
        var expiredBuffs = _buffs.Where(b => !b.IsActive).ToList();
        foreach (var buff in expiredBuffs)
        {
            buff.Remove(_stats);
            _buffs.Remove(buff);
        }
    }

    public void ProcessTurn()
    {
        foreach (var buff in _buffs.ToList())
        {
            if (buff.DecreaseTurn())
            {
                buff.Remove(_stats);
            }
        }

        // Limpa buffs expirados depois de processar o turno
        ClearExpiredBuffs();
    }

    public IEnumerable<Buff> GetBuffsByType(BuffType type)
    {
        return _buffs.Where(b => b.BuffType == type && b.IsActive);
    }
}