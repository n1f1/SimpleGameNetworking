using System.Threading.Tasks;

namespace Networking.Connection
{
    public interface IServerConnection
    {
        Task<bool> Connect();
    }
}