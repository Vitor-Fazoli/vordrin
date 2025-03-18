import { useState, useCallback } from 'react';

interface Player {
  id: string;
  name: string;
}

interface Enemy {
  id: string;
  name: string;
  health: number;
  healthMax: number;
}

interface GameState {
  isConnected: boolean;
  playerId: string;
  players: Player[];
  enemies: Enemy[];
}

export function useGameState() {
  const [gameState, setGameState] = useState<GameState>({
    isConnected: false,
    playerId: '',
    players: [],
    enemies: []
  });
  const [experience, setExperience] = useState(0);
  const [level, setLevel] = useState(1);
  const experienceToNextLevel = level * 100;

  const updateGameState = useCallback((newState: Partial<GameState>) => {
    setGameState(prevState => ({ ...prevState, ...newState }));
  }, []);

  return {
    gameState,
    updateGameState,
    enemies: gameState.enemies,
    experience,
    level,
    experienceToNextLevel
  };
}