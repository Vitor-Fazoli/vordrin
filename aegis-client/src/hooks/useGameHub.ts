import { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";

interface Message {
  userId: string;
  text: string;
}

const useGameHub = () => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [isConnected, setIsConnected] = useState<boolean>(false);
  const [messages, setMessages] = useState<Message[]>([]);

  useEffect(() => {
    // Inicializa a conexão com SignalR
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5189/gameHub") // Substitua pela URL do seu servidor SignalR
      .build();

    // Evento para receber mensagens do servidor
    connection.on("ReceiveUpdate", (message: string) => {
      setMessages((prevMessages) => [...prevMessages, { userId: "Server", text: message }]);
    });

    // Tente conectar
    connection.start()
      .then(() => {
        setIsConnected(true);
        console.log("Conectado ao SignalR!");
      })
      .catch((err) => {
        console.error("Erro ao conectar no SignalR: ", err);
      });

    setConnection(connection);

    return () => {
      if (connection) {
        connection.stop();
      }
    };
  }, []);

  const sendMessage = async (message: string) => {
    if (connection) {
      try {
        await connection.invoke("SendUpdate", message);
      } catch (err) {
        console.error("Erro ao enviar mensagem: ", err);
      }
    }
  };

  return { isConnected, messages, sendMessage };
};

export default useGameHub;