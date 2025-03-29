import { useState, useEffect } from "react";
import * as signalR from "@microsoft/signalr";

const Game = () => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [playerHealth, setPlayerHealth] = useState(100);
  const [enemyHealth, setEnemyHealth] = useState(100);
  const [roomId] = useState("sala123");

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5189/gamehub")
      .withAutomaticReconnect()
      .build();

    newConnection.start().then(() => {
      console.log("Conectado!");
      newConnection.invoke("CreateRoom", roomId);
    });

    newConnection.on("TakeDamage", (health) => {
      setPlayerHealth(health);
    });

    newConnection.on("EnemyAttacked", (health) => {
      setEnemyHealth(health);
    });

    newConnection.on("Dodged", () => {
      console.log("Esquivou!");
    });

    setConnection(newConnection);

    return () => {
      newConnection.stop();
    };
  }, []);

  const handleAttack = () => {
    if (connection) {
      connection.invoke("Attack", roomId);
    }
  };

  const handleDodge = () => {
    if (connection) {
      connection.invoke("Dodge", roomId);
    }
  };

  return (
    <div>
      <h1>Jogo</h1>
      <p>Vida do Jogador: {playerHealth}</p>
      <p>Vida do Inimigo: {enemyHealth}</p>
      <button onClick={handleAttack}>Atacar</button>
      <button onClick={handleDodge}>Esquivar</button>
    </div>
  );
};

export default Game;
