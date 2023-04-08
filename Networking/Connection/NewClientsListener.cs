using System;
using System.Net.Sockets;

namespace Networking.Connection
{
    public class NewClientsListener
    {
        private readonly Room _room;
        private readonly IClientConnection _clientConnection;

        private int _id;

        public NewClientsListener(Room room, IClientConnection clientConnection)
        {
            _room = room ?? throw new ArgumentNullException(nameof(room));
            _clientConnection = clientConnection;
        }

        public void ListenNewClients(TcpListener tcpListener)
        {
            if (tcpListener.Pending() == false)
                return;

            TcpClient tcpClient = AcceptTcpClient(tcpListener);
            Client client = new Client(tcpClient, ++_id);
            AddClient(client);
        }

        private void AddClient(Client client)
        {
            client.Welcome();
            _room.Add(client);
            _clientConnection.Connect(client);
        }

        private static TcpClient AcceptTcpClient(TcpListener tcpListener)
        {
            TcpClient tcpClient = tcpListener.AcceptTcpClient();
            tcpClient.NoDelay = true;
            tcpClient.SendBufferSize = 1500;
            Console.WriteLine("DemoGameClient connected...");

            return tcpClient;
        }
    }
}