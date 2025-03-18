export interface Enemy {
  id: string;
  name: string;
  health: number;
  healthMax: number;
}

export interface Player {
  id: string;
  name: string;
}

export interface GameState {
  isConnected: boolean;
  playerId: string;
  players: Player[];
  enemies: Enemy[];
}