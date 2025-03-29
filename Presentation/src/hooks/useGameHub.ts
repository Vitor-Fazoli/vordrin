import { useState, useEffect } from "react";
import * as signalR from "@microsoft/signalr";

const API_URL = "http://localhost:5189/gameHub"; // Ajuste conforme necessário

export function useGame() {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [playerHealth, setPlayerHealth] = useState(100);
  const [enemyHealth, setEnemyHealth] = useState(100);
  const [battleStarted, setBattleStarted] = useState(false);

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(API_URL, {
        withCredentials: true
      })
      .withAutomaticReconnect()
      .build();

    newConnection
      .start()
      .then(() => console.log("Conectado ao servidor!"))
      .catch(err => console.error("Erro na conexão:", err));

    newConnection.on("PlayerHealthUpdated", setPlayerHealth);
    newConnection.on("EnemyHealthUpdated", setEnemyHealth);
    newConnection.on("BattleStarted", () => setBattleStarted(true));
    newConnection.on("BattleEnded", message => {
      alert(message);
      setBattleStarted(false);
    });

    setConnection(newConnection);

    return () => {
      newConnection.stop();
    };
  }, []);

  const startBattle = async () => {
    if (connection) await connection.send("StartBattle");
  };

  const attack = async () => {
    if (connection) await connection.send("Attack");
  };

  const dodge = async () => {
    if (connection) await connection.send("Dodge");
  };

  return { playerHealth, enemyHealth, battleStarted, startBattle, attack, dodge };
}
