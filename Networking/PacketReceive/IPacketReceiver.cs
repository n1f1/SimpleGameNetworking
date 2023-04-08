using Networking.PacketReceive.Replication;
using Networking.StreamIO;

namespace Networking.PacketReceive
{
    public interface IPacketReceiver
    {
        void ReceivePackets(IInputStream inputStream, IReplicationPacketRead replicationPacketRead);
    }
}