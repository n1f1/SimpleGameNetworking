using Networking.Common.Replication;
using Networking.Common.Replication.ObjectCreationReplication;
using Networking.Common.StreamIO;

namespace Networking.Common.PacketReceive
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