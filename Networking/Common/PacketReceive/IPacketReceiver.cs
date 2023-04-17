using Networking.Common.Replication;
using Networking.Common.StreamIO;

namespace Networking.Common.PacketReceive
{
    public interface IPacketReceiver
    {
        void ReceivePackets(IInputStream inputStream, IReplicationPacketRead replicationPacketRead);
    }
}