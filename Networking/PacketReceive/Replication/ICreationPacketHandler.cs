using Networking.StreamIO;

namespace Networking.PacketReceive.Replication
{
    public interface ICreationPacketHandler
    {
        void Create(IInputStream inputStream);
    }
}