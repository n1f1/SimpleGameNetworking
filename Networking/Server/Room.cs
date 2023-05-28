using System.Collections.Generic;
using System.Linq;
using Networking.Common.PacketSend;

namespace Networking.Server
{
    public class Room
    {
        private readonly List<ServerClient> _clients = new();
        
        public IEnumerable<ServerClient> Clients => _clients.Where(client => client.IsConnected);

        public void Add(ServerClient serverClient)
        {
            _clients.Add(serverClient);
        }

        public void Send(INetworkPacket movePacket)
        {
            foreach (ServerClient client in Clients) 
                client.Sender.SendPacket(movePacket);
        }

        public void Remove(ServerClient serverClient)
        {
            _clients.Remove(serverClient);
        }
    }
}