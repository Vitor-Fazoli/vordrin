using Domain.Entities;
using Domain.Entities.Attributes;
using Domain.Enums;

namespace Domain.Factories.WeaponArquetypes;

public class Greatsword : WeaponBase
{
    private int _clickCounter;
    private bool _powerMode;
    private float _powerModeTimeRemaining;

    public Greatsword(string id, string name, string description, Damage baseDamage, int level)
        : base(id, name, description, WeaponType.Greatsword, baseDamage, level)
    {
        _clickCounter = 0;
        _powerMode = false;
        _powerModeTimeRemaining = 0f;

        Stats["ArmorPerBonus"] = new Armor(1f);
    }

    public override void OnLeftClick(Character user, Enemy target)
    {
        // LEMBRAR DE CALCULAR DANO AQUI

        int clickCount = _powerMode ? 2 : 1;

        target.TakeDamage(BaseDamage);

        // Incrementar contador de cliques
        _clickCounter += clickCount;

        // Verificar se atingiu 10 cliques para conceder armadura
        if (_clickCounter >= 10)
        {
            // Resetar contador e dar bônus de armadura
            _clickCounter -= 10;
            float armorBonus = Stats["ArmorPerBonus"].Get();
            //user.Attributes.Defense.Add(armorBonus);
        }
    }

    public override void OnRightClick(Character user)
    {
        // Ativar modo de poder por 10 segundos
        _powerMode = true;
        _powerModeTimeRemaining = 10f;
    }

    public override void Update(float deltaTime)
    {
        // Atualizar temporizador do modo de poder
        if (_powerMode)
        {
            _powerModeTimeRemaining -= deltaTime;

            if (_powerModeTimeRemaining <= 0f)
            {
                _powerMode = false;
            }
        }
    }
}