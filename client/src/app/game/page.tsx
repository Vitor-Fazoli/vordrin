"use client";

import PlayerTab from '@/components/player-tab';
import ProgressBar from '@/components/progress-bar';
import { useState, useEffect, useCallback } from 'react';
import { useGameState } from '@/hooks/useGameState';
import { useWebSocket } from '@/hooks/useWebSocket';
import { Enemy } from '@/types/game';

export default function GamePage() {
  const {
    gameState,
    updateGameState,
    enemies,
    experience,
    level,
    experienceToNextLevel
  } = useGameState();

  const socket = useWebSocket();

  const updateEnemyHealth = useCallback((enemyId: string, newHealth: number) => {
    const updatedEnemies = enemies.map((enemy: Enemy) =>
      enemy.id === enemyId ? { ...enemy, health: newHealth } : enemy
    );
    updateGameState({ ...gameState, enemies: updatedEnemies });
  }, [enemies, gameState, updateGameState]);

  // Handle incoming game state updates
  useEffect(() => {
    if (!socket) return;

    const handleGameStateUpdate = (newState: any) => {
      updateGameState(newState);
    };

    const handleEnemyDamaged = (data: { enemyId: string; newHealth: number; }) => {
      updateEnemyHealth(data.enemyId, data.newHealth);
    };

    socket.socket?.on('gameStateUpdate', handleGameStateUpdate);
    socket.socket?.on('enemyDamaged', handleEnemyDamaged);

    return () => {
      socket.socket?.off('gameStateUpdate', handleGameStateUpdate);
      socket.socket?.off('enemyDamaged', handleEnemyDamaged);
    };
  }, [socket, updateGameState, updateEnemyHealth]);

  const causeDamage = useCallback(() => {
    if (!socket || !enemies.length) return;

    const hitBtn = document.getElementById('hit-btn');
    hitBtn?.classList.add('animate-pulse');

    socket.socket?.emit('playerAttack', {
      enemyId: enemies[0].id,
      playerId: gameState.playerId,
      timestamp: Date.now()
    });

    setTimeout(() => {
      hitBtn?.classList.remove('animate-pulse');
    }, 10);
  }, [socket, enemies, gameState.playerId]);

  // Game loop for client-side predictions and animations
  const gameLoop = useCallback(() => {
    // Handle animations and visual updates
    // Implement client-side prediction here if needed
  }, []);

  useEffect(() => {
    const interval = setInterval(gameLoop, 1000 / 60);
    return () => clearInterval(interval);
  }, [gameLoop]);

  if (!gameState.isConnected) {
    return <div>Connecting to game server...</div>;
  }

  return (
    <div className='w-full h-full'>
      <div className="flex w-full">
        <aside className='bg-red-400 w-1/12 h-full'>
          <div className="players-list">
            {gameState.players.map(player => (
              <div key={player.id} className="player-item">
                {player.name}
              </div>
            ))}
          </div>
        </aside>
        <main className='w-11/12 h-full flex flex-col gap-5 justify-center items-center'>
          <div className='w-1/2 pt-4'>
            <ProgressBar
              label={enemies[0]?.name || 'No enemy'}
              progress={enemies[0]?.health || 0}
              maxValue={enemies[0]?.healthMax || 100}
            />
          </div>
          <PlayerTab />
        </main>
        <button
          id='hit-btn'
          className='bg-rose-700 py-5 px-9'
          onClick={causeDamage}
          disabled={!gameState.isConnected || !enemies.length}
        >
          HIT!
        </button>
      </div>
      <div className="absolute bottom-0 w-full">
        <p className='pl-2'>Level: <span>{level}</span></p>
        <ProgressBar
          progress={experience}
          maxValue={experienceToNextLevel}
          small={true}
        />
      </div>
    </div>
  );
}
