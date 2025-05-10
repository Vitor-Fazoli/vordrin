using System.Diagnostics;
using Domain.Entities;
using Domain.Entities.Attributes;
using Domain.Enums;

namespace Domain.Factories.WeaponArquetypes;

public class Pistol : WeaponBase
{
    private int _currentAmmo;
    private int _maxAmmo;
    private float _reloadTime;
    private bool _isReloading;
    private float _reloadTimeLeft;

    public Pistola(string id, string name, string description, Damage baseDamage,
                  float attackSpeed, int level)
        : base(id, name, description, WeaponType.Pistol, baseDamage, attackSpeed, level)
    {
        _maxAmmo = 17;
        _currentAmmo = _maxAmmo;
        _reloadTime = 0.5f;
        _isReloading = false;
    }

    public override void OnLeftClick(Character user, Enemy target)
    {
        // Verificar se tem munição e não está recarregando
        if (_currentAmmo <= 0 || _isReloading)
        {
            Debug.WriteLine(_currentAmmo <= 0 ? "Sem munição! Recarregue (botão direito)." : "Recarregando...");
            return;
        }

        target.TakeDamage(CalculateDamage());
        _currentAmmo--;

        Debug.WriteLine($"{Name} atirou! Munição restante: {_currentAmmo}/{_maxAmmo}");
    }

    public override void OnRightClick(Character user)
    {
        // Iniciar recarga se não estiver recarregando e não estiver com munição cheia
        if (!_isReloading && _currentAmmo < _maxAmmo)
        {
            _isReloading = true;
            _reloadTimeLeft = _reloadTime;
            Debug.WriteLine($"Recarregando {Name}...");
        }
    }

    public override void Update(float deltaTime)
    {
        if (_isReloading)
        {
            _reloadTimeLeft -= deltaTime;

            if (_reloadTimeLeft <= 0f)
            {
                _currentAmmo = _maxAmmo;
                _isReloading = false;
                Debug.WriteLine($"{Name} recarregada! {_currentAmmo}/{_maxAmmo} balas.");
            }
        }
    }
}
