using System.Diagnostics;
using System.Numerics;
using Domain.Entities;
using Domain.Entities.Attributes;
using Domain.Enums;
using Domain.Helper;
using Domain.Interfaces;

namespace Domain.Factories.WeaponArquetypes;

public class Nerali : WeaponBase
{
    private bool _isHealingMode;
    private Stopwatch _chargeStopwatch = new();
    private bool _isCharging;
    private readonly float _maxChargeTime = 3.0f;

    public Nerali(string id, string name, string description, Damage baseDamage, int level)
        : base(id, name, description, WeaponType.Nerali, baseDamage, level)
    {
        _isHealingMode = false;
        _isCharging = false;

        Stats["MaxPoisonDamage"] = baseDamage.Multiply(2f);
        Stats["MaxHealAmount"] = baseDamage.Multiply(1.5f);
    }

    // Começar a carregar o tiro
    public void StartCharge()
    {
        _isCharging = true;
        _chargeStopwatch.Restart();
    }

    // Liberar o tiro carregado
    public void ReleaseCharge(Character user, IDamageable target)
    {
        if (!_isCharging) return;

        float chargeTime = MathF.Min((float)_chargeStopwatch.Elapsed.TotalSeconds, _maxChargeTime);
        float chargePercentage = chargeTime / _maxChargeTime;

        if (_isHealingMode && target is Character allyTarget)
        {
            float healPoints = Mathematics.Lerp(BaseDamage.Get(), Stats["MaxHealAmount"].Get(), chargePercentage);

            Heal Heal = new(healPoints, BaseDamage.CriticalChance.Get(), BaseDamage.CriticalMultiplier.Get());

            allyTarget.ReceiveHeal(Heal);
            Debug.WriteLine($"{Name} curou {allyTarget.Name} em {healPoints} pontos!");
        }
        else if (!_isHealingMode && target is Enemy enemyTarget)
        {
            enemyTarget.TakeDamage(BaseDamage);
            Debug.WriteLine($"{Name} envenenou {enemyTarget.Name} causando {BaseDamage} de dano ao longo do tempo!");
        }

        _isCharging = false;
    }

    public override void OnLeftClick(Character user, Enemy target)
    {
        // Simulação simplificada: ao invés de segurar o botão, apenas iniciamos a carga
        StartCharge();
    }

    public override void OnRightClick(Character user)
    {
        // Alternar entre modos de cura e veneno
        _isHealingMode = !_isHealingMode;
        string mode = _isHealingMode ? "cura" : "veneno";
        Debug.WriteLine($"{Name} alternou para o modo de {mode}.");
    }

    public override void Update(float deltaTime)
    {
        // Atualização de efeitos visuais de carga iria aqui (não implementado)
    }
}