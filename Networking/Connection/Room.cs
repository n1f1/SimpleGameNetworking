using System.Collections.Generic;
using System.Linq;

namespace Networking.Connection
{
    public class Room
    {
        private readonly List<Client> _clients = new();
        
        public IEnumerable<Client> Clients => _clients.Where(client => client.IsConnected);

        public void Add(Client client)
        {
            _clients.Add(client);
        }
    }
}