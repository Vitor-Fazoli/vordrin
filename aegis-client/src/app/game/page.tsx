'use client'

import React, { useEffect, useState } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";

const GamePage: React.FC = () => {
  const [enemyHp, setEnemyHp] = useState(100);
  const [connection, setConnection] = useState<any>(null);
  const [sessionId, setSessionId] = useState("room123");

  useEffect(() => {
    // Conectando-se ao SignalR Hub
    const newConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:5189/gameHub")  // URL do backend SignalR
      .build();

    // Iniciar a conexão
    newConnection.start()
      .then(() => {
        console.log("Conectado ao SignalR");
      })
      .catch((err: any) => console.log("Erro ao conectar ao SignalR:", err));

    setConnection(newConnection);

    // Ouvir a atualização do HP do inimigo via SignalR
    newConnection.on("ReceiveEnemyHp", (newHp: number) => {
      setEnemyHp(newHp);
    });

    return () => {
      newConnection.stop();
    };
  }, []);

  // Função para lidar com o clique de ataque
  const handleAttack = () => {
    // Envia uma mensagem de ataque para o servidor via SignalR
    if (connection) {
      connection.invoke("Attack", sessionId, 10)  // Enviar dano de 10 como exemplo
        .catch((err: any) => console.error("Erro ao enviar ataque:", err));
    }
  };

  return (
    <div>
      <h1>Clicker RPG Multiplayer</h1>
      <p>Id da sala: {sessionId}</p>
      <h2>Vida do inimigo: {enemyHp}</h2>
      <progress value={enemyHp} max="100"></progress>
      <br />
      <button onClick={handleAttack}>Atacar!</button>
    </div>
  );
};

export default GamePage;
