using System;
using System.Net.Sockets;

namespace Networking.Server.Connection
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
            ServerClient serverClient = new ServerClient(tcpClient, ++_id);
            AddClient(serverClient);
        }

        private void AddClient(ServerClient serverClient)
        {
            serverClient.Welcome();
            _room.Add(serverClient);
            _clientConnection.Connect(serverClient);
        }

        private static TcpClient AcceptTcpClient(TcpListener tcpListener)
        {
            TcpClient tcpClient = tcpListener.AcceptTcpClient();
            tcpClient.NoDelay = true;
            tcpClient.SendBufferSize = 1500;

            return tcpClient;
        }
    }
}