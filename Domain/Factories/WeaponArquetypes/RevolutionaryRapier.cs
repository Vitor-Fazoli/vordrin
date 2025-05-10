using System.Diagnostics;
using Domain.Entities;
using Domain.Entities.Attributes;
using Domain.Enums;

public class RevolutionaryRapier : WeaponBase
{
    private int _hitCounter;
    private float _flagBuffDuration;
    private float _flagBuffTimeRemaining;
    private bool _flagActive;

    public RevolutionaryRapier(string id, string name, string description, Damage baseDamage,
                  float attackSpeed, int level)
        : base(id, name, description, WeaponType.RevolutionaryRapier, baseDamage, attackSpeed, level)
    {
        _hitCounter = 0;
        _flagBuffDuration = 8.0f;
        _flagBuffTimeRemaining = 0f;
        _flagActive = false;

        //Stats["WeakenAmount"] = 0.25f;
        //Stats["FlagDamageBoost"] = 0.3f + (level * 0.02f);
    }

    public override void OnLeftClick(Character user, Enemy target)
    {
        // Aplicar dano ao inimigo
        target.TakeDamage(BaseDamage);

        // Incrementar contador de acertos
        _hitCounter++;

        if (_hitCounter >= 20)
        {
            _hitCounter = 0;
            //target.ApplyWeaken(Stats["WeakenAmount"], 5f);
            Debug.WriteLine($"{target.Name} foi enfraquecido pelo {Name}!");
        }
    }

    public override void OnRightClick(Character user)
    {
        _flagActive = true;
        _flagBuffTimeRemaining = _flagBuffDuration;

        //user.ApplyDamageBoost(Stats["FlagDamageBoost"], _flagBuffDuration);

        // foreach (Character ally in user.NearbyAllies)
        // {
        //     ally.ApplyDamageBoost(Stats["FlagDamageBoost"], _flagBuffDuration);
        // }

        Debug.WriteLine($"{user.Name} ergueu a bandeira com {Name}! Dano aumentado por {_flagBuffDuration} segundos.");
    }

    public override void Update(float deltaTime)
    {
        if (_flagActive)
        {
            _flagBuffTimeRemaining -= deltaTime;

            if (_flagBuffTimeRemaining <= 0f)
            {
                _flagActive = false;
                Debug.WriteLine($"Efeito da bandeira do {Name} acabou!");
            }
        }
    }
}