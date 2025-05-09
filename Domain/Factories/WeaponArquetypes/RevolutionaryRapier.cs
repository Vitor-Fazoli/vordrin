public class Florete : WeaponBase
{
    private int _hitCounter;
    private float _flagBuffDuration;
    private float _flagBuffTimeRemaining;
    private bool _flagActive;

    public Florete(string id, string name, string description, float baseDamage,
                  float attackSpeed, int level, Sprite weaponSprite)
        : base(id, name, description, WeaponType.Florete, baseDamage, attackSpeed, level, weaponSprite)
    {
        _hitCounter = 0;
        _flagBuffDuration = 8.0f;
        _flagBuffTimeRemaining = 0f;
        _flagActive = false;

        // Stats específicos do Florete
        Stats["WeakenAmount"] = 0.25f; // Redução de 25% na força do inimigo
        Stats["FlagDamageBoost"] = 0.3f + (level * 0.02f); // Aumento de 30% + 2% por nível no dano
    }

    public override void OnLeftClick(Player user, Enemy target)
    {
        float damage = CalculateDamage();

        // Aplicar bônus de dano se a bandeira estiver ativa
        if (_flagActive)
        {
            damage *= (1f + Stats["FlagDamageBoost"]);
        }

        // Aplicar dano ao inimigo
        target.TakeDamage(damage);

        // Incrementar contador de acertos
        _hitCounter++;

        // Verificar se atingiu 20 acertos para enfraquecer o inimigo
        if (_hitCounter >= 20)
        {
            _hitCounter = 0;
            target.ApplyWeaken(Stats["WeakenAmount"], 5f); // Enfraquecer por 5 segundos
            Debug.Log($"{target.Name} foi enfraquecido pelo {Name}!");
        }
    }

    public override void OnRightClick(Player user)
    {
        // Ativar buff de bandeira
        _flagActive = true;
        _flagBuffTimeRemaining = _flagBuffDuration;

        // Aplicar buff ao jogador e aliados próximos
        user.ApplyDamageBoost(Stats["FlagDamageBoost"], _flagBuffDuration);

        // Aplicar a aliados (em um jogo real, você procuraria por aliados próximos)
        foreach (Player ally in user.NearbyAllies)
        {
            ally.ApplyDamageBoost(Stats["FlagDamageBoost"], _flagBuffDuration);
        }

        Debug.Log($"{user.Name} ergueu a bandeira com {Name}! Dano aumentado por {_flagBuffDuration} segundos.");
    }

    public override void Update(float deltaTime)
    {
        // Atualizar temporizador do buff da bandeira
        if (_flagActive)
        {
            _flagBuffTimeRemaining -= deltaTime;

            if (_flagBuffTimeRemaining <= 0f)
            {
                _flagActive = false;
                Debug.Log($"Efeito da bandeira do {Name} acabou!");
            }
        }
    }
}