using Networking.Common.StreamIO;

namespace Networking.Common.Replication
{
    public interface IReplicationPacketRead
    {
        void ProcessReplicationPacket(IInputStream inputStream);
    }
}