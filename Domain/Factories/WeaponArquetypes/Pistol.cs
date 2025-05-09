public class Pistola : WeaponBase
{
    private int _currentAmmo;
    private int _maxAmmo;
    private float _reloadTime;
    private bool _isReloading;
    private float _reloadTimeLeft;

    public Pistola(string id, string name, string description, float baseDamage,
                  float attackSpeed, int level, Sprite weaponSprite)
        : base(id, name, description, WeaponType.Pistola, baseDamage, attackSpeed, level, weaponSprite)
    {
        _maxAmmo = 17;
        _currentAmmo = _maxAmmo;
        _reloadTime = 0.5f;
        _isReloading = false;

        // Stats específicos da Pistola
        Stats["CritChance"] = 0.1f + (level * 0.01f); // 10% + 1% por nível de chance de crítico
        Stats["CritDamage"] = 2.0f; // Multiplicador de dano crítico
    }

    public override void OnLeftClick(Player user, Enemy target)
    {
        // Verificar se tem munição e não está recarregando
        if (_currentAmmo <= 0 || _isReloading)
        {
            Debug.Log(_currentAmmo <= 0 ? "Sem munição! Recarregue (botão direito)." : "Recarregando...");
            return;
        }

        // Calcular dano, com chance de crítico
        float damage = CalculateDamage();
        bool isCritical = UnityEngine.Random.value < Stats["CritChance"];

        if (isCritical)
        {
            damage *= Stats["CritDamage"];
            Debug.Log($"Acerto crítico! {damage} de dano.");
        }

        // Aplicar dano e consumir munição
        target.TakeDamage(damage);
        _currentAmmo--;

        Debug.Log($"{Name} atirou! Munição restante: {_currentAmmo}/{_maxAmmo}");
    }

    public override void OnRightClick(Player user)
    {
        // Iniciar recarga se não estiver recarregando e não estiver com munição cheia
        if (!_isReloading && _currentAmmo < _maxAmmo)
        {
            _isReloading = true;
            _reloadTimeLeft = _reloadTime;
            Debug.Log($"Recarregando {Name}...");
        }
    }

    public override void Update(float deltaTime)
    {
        // Processar recarga
        if (_isReloading)
        {
            _reloadTimeLeft -= deltaTime;

            if (_reloadTimeLeft <= 0f)
            {
                _currentAmmo = _maxAmmo;
                _isReloading = false;
                Debug.Log($"{Name} recarregada! {_currentAmmo}/{_maxAmmo} balas.");
            }
        }
    }
}
