using System.Diagnostics;
using Domain.Entities;
using Domain.Entities.Attributes;
using Domain.Enums;

namespace Domain.Factories.WeaponArquetypes;

public class Nerali : WeaponBase
{
    private bool _isHealingMode;
    private float _chargeStartTime;
    private bool _isCharging;
    private float _maxChargeTime = 3.0f; // Tempo máximo de carga (3 segundos)

    public Nerali(string id, string name, string description, Damage baseDamage,
                 float attackSpeed, int level)
        : base(id, name, description, WeaponType.Nerali, baseDamage, attackSpeed, level)
    {
        _isHealingMode = false;
        _isCharging = false;

        // Stats específicos do Nerali
        Stats["MaxPoisonDamage"] = baseDamage * 3.0f; // Dano máximo de veneno quando totalmente carregado
        Stats["MaxHealAmount"] = baseDamage * 2.5f;   // Cura máxima quando totalmente carregado
    }

    // Começar a carregar o tiro
    public void StartCharge()
    {
        _isCharging = true;
        _chargeStartTime = Time.time;
    }

    // Liberar o tiro carregado
    public void ReleaseCharge(Character user, Character target)
    {
        if (!_isCharging) return;

        float chargeTime = MathF.Min(Time.time - _chargeStartTime, _maxChargeTime);
        float chargePercentage = chargeTime / _maxChargeTime;

        if (_isHealingMode && target is Player allyTarget)
        {
            // Modo de cura (para aliados)
            float healAmount = Mathf.Lerp(BaseDamage, Stats["MaxHealAmount"], chargePercentage);
            allyTarget.Heal(healAmount);
            Debug.WriteLine($"{Name} curou {allyTarget.Name} em {healAmount} pontos!");
        }
        else if (!_isHealingMode && target is Enemy enemyTarget)
        {
            // Modo de veneno (para inimigos)
            float poisonDamage = Mathf.Lerp(BaseDamage, Stats["MaxPoisonDamage"], chargePercentage);
            enemyTarget.ApplyPoison(poisonDamage, 5f); // Aplica veneno por 5 segundos
            Debug.WriteLine($"{Name} envenenou {enemyTarget.Name} causando {poisonDamage} de dano ao longo do tempo!");
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