import { Weapon } from "./weapon";

export class Player {
    public name: string;
    public level: number;
    public xp: number;
    public health: number;
    public maxHealth: number;
    public damage: number;
    public weaponSlots: (Weapon | null)[] = [null, null];

    constructor(
        name: string,
        level: number,
        xp: number,
        health: number,
        maxHealth: number,
        damage: number,
        weapon?: Weapon
    ) {
        this.name = name;
        this.level = level;
        this.xp = xp;
        this.health = health;
        this.maxHealth = maxHealth;
        this.damage = damage;
        if (weapon) {
            this.equipWeapon(weapon, 0);
        }
    }

    public equipWeapon(weapon: Weapon, slot: 0 | 1): void {
        if (weapon.twoHanded) {
            this.weaponSlots[0] = weapon;
            this.weaponSlots[1] = null;
        } else {
            if (slot === 0) {
                if (!this.weaponSlots[0]?.twoHanded) {
                    this.weaponSlots[0] = weapon;
                }
            } else if (slot === 1) {
                if (!this.weaponSlots[0]?.twoHanded) {
                    this.weaponSlots[1] = weapon;
                }
            }
        }

        this.damage = this.weaponSlots.reduce((total, weapon) => total + (weapon?.damage || 0), 0);
    }
}
