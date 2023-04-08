using System.Collections.Generic;

namespace Networking.Connection
{
    public class Room
    {
        private readonly List<Client> _clients = new();
        
        public IEnumerable<Client> Clients => _clients;

        public void Add(Client client)
        {
            _clients.Add(client);
        }
    }
}