using Networking.StreamIO;

namespace Networking.Replication
{
    public interface IReplicationPacketRead
    {
        void ProcessReplicationPacket(IInputStream inputStream);
    }
}