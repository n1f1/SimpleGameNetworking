using System;
using System.Reflection;
using Networking.Common.Packets;
using Networking.Common.PacketSend;

namespace Networking.Common.Replication.ObjectCreationReplication
{
    public class ObjectReplicationPacketFactory
    {
        private readonly IGenericInterfaceList _serialization;
        private readonly ITypeIdConversion _typeIdConversion;

        public ObjectReplicationPacketFactory(IGenericInterfaceList serialization,
            ITypeIdConversion typeIdConversion)
        {
            _serialization = serialization ?? throw new ArgumentNullException(nameof(serialization));
            _typeIdConversion = typeIdConversion ?? throw new ArgumentNullException(nameof(typeIdConversion));
        }

        public INetworkPacket Create<TObject>(TObject replicatingObject)
        {
            IPacketHeader header = new ReplicationPacketHeader(ReplicationType.CreateObject);
            MemoryNetworkPacket packet = new MemoryNetworkPacket(header);
            Type type = typeof(TObject);

            if (_serialization.ContainsForType(type) == false)
                throw new InvalidOperationException();

            object serialization = _serialization.GetForType(type);

            MethodInfo method = serialization.GetType().GetMethod("Serialize");
            packet.OutputStream.Write(_typeIdConversion.GetTypeID<TObject>());
            method.Invoke(serialization, new object[] {replicatingObject, packet.OutputStream});
            packet.Close();

            return packet;
        }
    }
}