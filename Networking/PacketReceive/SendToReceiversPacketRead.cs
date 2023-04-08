using System;
using System.Collections.Generic;
using Networking.PacketReceive.Replication;
using Networking.PacketReceive.Replication.ObjectCreationReplication;
using Networking.PacketReceive.Replication.Serialization;
using Networking.StreamIO;

namespace Networking.PacketReceive
{
    public class SendToReceiversPacketRead : IReplicationPacketRead
    {
        private readonly ReplicationPacketRead _replicationPacketRead;

        public SendToReceiversPacketRead(Dictionary<Type, object> receivers,
            IEnumerable<(Type, object)> deserializationValues, TypeIdConversion typeId)
        {
            Dictionary<Type, IDeserialization<object>> deserialization = new();
            deserialization.PopulateDictionary(deserializationValues);

            _replicationPacketRead = new ReplicationPacketRead(new CreationReplicator(typeId, deserialization,
                new ReceivedReplicatedObjectMatcher(receivers)));
        }

        public void ProcessReplicationPacket(IInputStream inputStream) => 
            _replicationPacketRead.ProcessReplicationPacket(inputStream);
    }
}