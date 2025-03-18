"use client";

import { useState, useEffect } from 'react';
import { FaGamepad, FaChartBar } from 'react-icons/fa';
import { Player } from '../entities/player';
import { Weapon } from '../entities/weapon';

export default function Home() {
  const [damage, setDamage] = useState(1);
  const [xp, setXp] = useState(0);
  const [level, setLevel] = useState(1);
  const [enemyHealth, setEnemyHealth] = useState(10);
  const [maxEnemyHealth, setMaxEnemyHealth] = useState(10);
  const [enemyType, setEnemyType] = useState('Goblin');
  const [kills, setKills] = useState(0);
  const [activeTab, setActiveTab] = useState('main');
  const [player, setPlayer] = useState(new Player('Hero', 1, 0, 100, 100, 1));

  const weapons = [
    new Weapon('Greatsword', 2, true),
    new Weapon('Sword', 1, false),
    new Weapon('Dagger', 1, false), 
    new Weapon('Battle Axe', 3, true),
    new Weapon('Mace', 1, false)
  ];

  const enemies = [
    { type: 'Goblin', health: 10, xpReward: 5 },
    { type: 'Skeleton', health: 15, xpReward: 8 },
    { type: 'Orc', health: 25, xpReward: 15 },
    { type: 'Troll', health: 50, xpReward: 30 },
    { type: 'Dragon', health: 100, xpReward: 60 }
  ];

  const equipWeapon = (weapon: Weapon, slot: 0 | 1) => {
    const newPlayer = new Player(
      player.name,
      player.level,
      player.xp,
      player.health,
      player.maxHealth,
      player.damage
    );
    newPlayer.equipWeapon(weapon, slot);
    setPlayer(newPlayer);
    setDamage(newPlayer.damage);
  };

  const handleClick = () => {
    const newHealth = enemyHealth - player.damage;
    setEnemyHealth(newHealth);

    if (newHealth <= 0) {
      // Enemy defeated
      const currentEnemy = enemies.find(e => e.type === enemyType);
      if (currentEnemy) {
        const newXp = xp + currentEnemy.xpReward;
        setXp(newXp);
        setKills(kills + 1);

        // Level up check
        if (newXp >= level * 20) {
          setLevel(level + 1);
          // Instead of directly setting damage, we'll get a new weapon option
          if (level % 2 === 0 && level < weapons.length * 2) {
            const newWeapon = weapons[Math.floor(level / 2)];
            equipWeapon(newWeapon, 0);
          }
        }
      }

      // Spawn new enemy
      const nextEnemyIndex = Math.min(Math.floor(kills / 5), enemies.length - 1);
      const nextEnemy = enemies[nextEnemyIndex];
      setEnemyType(nextEnemy.type);
      setMaxEnemyHealth(nextEnemy.health);
      setEnemyHealth(nextEnemy.health);
    }
  };

  return (
      <div className="game-container">
        <h1>Codename Aegis</h1>
      </div>
  );
}