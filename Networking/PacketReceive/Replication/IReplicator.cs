using Networking.StreamIO;

namespace Networking.PacketReceive.Replication
{
    public interface IReplicationPacketRead
    {
        void ProcessReplicationPacket(IInputStream inputStream);
    }
}