using Networking.StreamIO;

namespace Networking.Replication
{
    public interface ICreationPacketHandler
    {
        void Create(IInputStream inputStream);
    }
}