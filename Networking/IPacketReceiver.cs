using Networking.Replication;
using Networking.StreamIO;

namespace Networking
{
    public interface IPacketReceiver
    {
        void ReceivePackets(IInputStream inputStream, IReplicationPacketRead replicationPacketRead);
    }
}