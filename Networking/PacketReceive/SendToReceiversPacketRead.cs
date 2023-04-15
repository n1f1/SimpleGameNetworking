using Networking.PacketReceive.Replication;
using Networking.PacketReceive.Replication.ObjectCreationReplication;
using Networking.StreamIO;

namespace Networking.PacketReceive
{
    public class SendToReceiversPacketRead : IReplicationPacketRead
    {
        private readonly ReplicationPacketRead _replicationPacketRead;

        public SendToReceiversPacketRead(IGenericInterfaceList receivers,
            IGenericInterfaceList deserialization, ITypeIdConversion typeId)
        {
            _replicationPacketRead = new ReplicationPacketRead(new CreationReplicator(typeId, deserialization,
                new ReceivedReplicatedObjectMatcher(receivers)));
        }

        public void ProcessReplicationPacket(IInputStream inputStream) => 
            _replicationPacketRead.ProcessReplicationPacket(inputStream);
    }
}