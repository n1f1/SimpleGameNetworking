using System;
using System.Reflection;
using Networking.Common.Packets;
using Networking.Common.PacketSend;
using Networking.Common.Replication.Serialization;
using Object = UnityEngine.Object;

namespace Networking.Common.Replication.ObjectCreationReplication
{
    public class ObjectReplicationPacketFactory
    {
        private readonly IGenericInterfaceList _serialization;
        private readonly ITypeIdConversion _typeIdConversion;
        private readonly Type _type = typeof(ISerialization<>);

        public ObjectReplicationPacketFactory(IGenericInterfaceList serialization,
            ITypeIdConversion typeIdConversion)
        {
            _serialization = serialization ?? throw new ArgumentNullException(nameof(serialization));

            if (_serialization.InterfaceType != _type)
                throw new ArgumentException($"{serialization} must have InterfaceType property of type {_type}");
                    
            _typeIdConversion = typeIdConversion ?? throw new ArgumentNullException(nameof(typeIdConversion));
        }

        public INetworkPacket Create<TObject>(TObject replicatingObject)
        {
            IPacketHeader header = new ReplicationPacketHeader(ReplicationType.CreateObject);
            MemoryNetworkPacket packet = new MemoryNetworkPacket(header);
            Type type = typeof(TObject);

            if (_serialization.ContainsForType(type) == false)
                throw new InvalidOperationException();
            
            WriteTypeToStream<TObject>(packet);
            SerializeObjectToStream(replicatingObject, type, packet);
            packet.Close();

            return packet;
        }

        private void WriteTypeToStream<TObject>(MemoryNetworkPacket packet) => 
            packet.OutputStream.Write(_typeIdConversion.GetTypeID<TObject>());

        private void SerializeObjectToStream<TObject>(TObject replicatingObject, Type type, MemoryNetworkPacket packet)
        {
            object serialization = _serialization.GetForType(type);
            MethodInfo method = serialization.GetType().GetMethod(nameof(ISerialization<object>.Serialize));
            method.Invoke(serialization, new object[] {replicatingObject, packet.OutputStream});
        }
    }
}