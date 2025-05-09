using Domain.Entities.Attributes;

public static class WeaponFactory
{
    private static readonly Dictionary<string, WeaponBase> _weaponTemplates = [];

    // Registrar um template de arma
    public static void RegisterWeapon(WeaponBase weapon)
    {
        if (!_weaponTemplates.ContainsKey(weapon.Id))
        {
            _weaponTemplates.Add(weapon.Id, weapon);
        }
    }

    // Criar uma instância de arma a partir de um template
    public static WeaponBase? CreateWeapon(string weaponId)
    {
        if (_weaponTemplates.TryGetValue(weaponId, out WeaponBase? template))
        {
            return template;
        }

        return null;
    }

    // Criar uma nova instância de Montante
    public static Greatsword CreateMontante(string id, string name, string description, Damage baseDamage,
                                         float attackSpeed, int level)
    {
        return new Greatsword(id, name, description, baseDamage, attackSpeed, level);
    }

    // Criar uma nova instância de Nerali
    public static Nerali CreateNerali(string id, string name, string description, float baseDamage,
                                     float attackSpeed, int level, Sprite weaponSprite)
    {
        return new Nerali(id, name, description, baseDamage, attackSpeed, level, weaponSprite);
    }

    // Criar uma nova instância de Pistola
    public static Pistola CreatePistola(string id, string name, string description, float baseDamage,
                                      float attackSpeed, int level, Sprite weaponSprite)
    {
        return new Pistola(id, name, description, baseDamage, attackSpeed, level, weaponSprite);
    }

    // Criar uma nova instância de Florete
    public static Florete CreateFlorete(string id, string name, string description, float baseDamage,
                                      float attackSpeed, int level, Sprite weaponSprite)
    {
        return new Florete(id, name, description, baseDamage, attackSpeed, level, weaponSprite);
    }

    // Inicialização com algumas armas de exemplo
    static WeaponFactory()
    {
        // Exemplos de Montante
        RegisterWeapon(new Montante(
            "montante_iron", "Montante de Ferro",
            "Uma espada montante básica forjada em ferro.",
            15f, 1.2f, 1, null
        ));

        RegisterWeapon(new Montante(
            "montante_legendary", "Lâmina do Conquistador",
            "Um montante lendário que já derrubou muitos exércitos.",
            35f, 1.5f, 10, null
        ));

        // Exemplos de Nerali
        RegisterWeapon(new Nerali(
            "nerali_basic", "Nerali do Curandeiro",
            "Um arco Nerali básico que alterna entre flechas curativas e venenosas.",
            12f, 1.5f, 1, null
        ));

        RegisterWeapon(new Nerali(
            "nerali_ancient", "Nerali dos Ancestrais",
            "Um arco Nerali antigo com potentes flechas encantadas.",
            25f, 1.8f, 8, null
        ));

        // Exemplos de Pistola
        RegisterWeapon(new Pistola(
            "pistol_standard", "Pistola Padrão",
            "Uma pistola confiável com 17 balas por pente.",
            20f, 2.0f, 1, null
        ));

        RegisterWeapon(new Pistola(
            "pistol_enhanced", "Pistola Reforçada",
            "Uma versão aprimorada com maior poder de fogo.",
            30f, 2.2f, 5, null
        ));

        // Exemplos de Florete
        RegisterWeapon(new Florete(
            "florete_basic", "Florete Revolucionário",
            "Um florete que inspira revolução e enfraquece inimigos.",
            14f, 1.8f, 1, null
        ));

        RegisterWeapon(new Florete(
            "florete_champion", "Florete do Campeão",
            "Um florete aprimorado que lidera a revolução com maior eficácia.",
            28f, 2.0f, 7, null
        ));
    }
}