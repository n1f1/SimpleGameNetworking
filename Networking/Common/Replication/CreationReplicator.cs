using System;
using Networking.Common.PacketReceive;
using Networking.Common.Replication.ObjectCreationReplication;
using Networking.Common.Replication.Serialization;
using Networking.Common.StreamIO;

namespace Networking.Common.Replication
{
    public class CreationReplicator : ICreationPacketHandler
    {
        private readonly ITypeIdConversion _typeIdConversion;
        private readonly IGenericInterfaceList _deserializationList;
        private readonly IReplicatedObjectReceiver<object> _replicatedObjectReceiver;

        public CreationReplicator(ITypeIdConversion typeIdConversion,
            IGenericInterfaceList deserializationList,
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

            if (_deserializationList.ContainsForType(creatingType) == false)
                throw new InvalidOperationException();

            IDeserialization<object> deserialization =
                (IDeserialization<object>) _deserializationList.GetForType(creatingType);
            
            object createdObject = deserialization.Deserialize(inputStream);
            _replicatedObjectReceiver.Receive(createdObject);
        }
    }
}