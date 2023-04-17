using System.Threading.Tasks;

namespace Networking.Client.Connection
{
    public interface IServerConnection
    {
        Task<bool> Connect();
    }
}