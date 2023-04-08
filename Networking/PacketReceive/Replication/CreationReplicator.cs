using System;
using System.Collections.Generic;
using Networking.PacketReceive.Replication.ObjectCreationReplication;
using Networking.PacketReceive.Replication.Serialization;
using Networking.StreamIO;

namespace Networking.PacketReceive.Replication
{
    public class CreationReplicator : ICreationPacketHandler
    {
        private readonly ITypeIdConversion _typeIdConversion;
        private readonly Dictionary<Type, IDeserialization<object>> _deserializationList;
        private readonly IReplicatedObjectReceiver<object> _replicatedObjectReceiver;

        public CreationReplicator(ITypeIdConversion typeIdConversion,
            Dictionary<Type, IDeserialization<object>> deserializationList,
            IReplicatedObjectReceiver<object> replicatedObjectReceiver)
        {
            _typeIdConversion = typeIdConversion;
            _deserializationList = deserializationList;
            _replicatedObjectReceiver = replicatedObjectReceiver;
        }

        public void Create(IInputStream inputStream)
        {
            int classId = inputStream.ReadInt32();
            Type creatingType = _typeIdConversion.GetTypeByID(classId);

            if (_deserializationList.ContainsKey(creatingType) == false)
                throw new InvalidOperationException();
            
            IDeserialization<object> deserialization = _deserializationList[creatingType];
            object createdObject = deserialization.Deserialize(inputStream);
            _replicatedObjectReceiver.Receive(createdObject);
        }
    }
}