using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Networking.Packets;
using Networking.PacketSender;

namespace Networking.Replication.ObjectCreationReplication
{
    public class ObjectReplicationPacketFactory
    {
        private readonly Dictionary<Type, object> _serialization;
        private readonly ITypeIdConversion _typeIdConversion;

        public ObjectReplicationPacketFactory(Dictionary<Type, object> serialization,
            ITypeIdConversion typeIdConversion)
        {
            foreach (var keyValuePair in serialization)
            {
                Type value = keyValuePair.Value.GetType();
                Type key = keyValuePair.Key;
                
                if (value.GetInterfaces().Any(interfaceType =>
                    interfaceType.IsGenericType &&
                    interfaceType.GetGenericArguments()[0] == key))
                    continue;

                throw new ArgumentException(
                    "Serialization dictionary must contain <Type, object> where object is ISerialization<Type>");
            }

            _serialization = serialization;
            _typeIdConversion = typeIdConversion ?? throw new ArgumentNullException(nameof(typeIdConversion));
        }

        public INetworkPacket Create<TObject>(TObject replicatingObject)
        {
            IPacketHeader header = new ReplicationPacketHeader(ReplicationType.CreateObject);
            MemoryNetworkPacket packet = new MemoryNetworkPacket(header);
            Type type = typeof(TObject);

            if (_serialization.ContainsKey(type) == false)
                throw new InvalidOperationException();

            object serialization = _serialization[type];

            MethodInfo method = serialization.GetType().GetMethod("Serialize");
            packet.OutputStream.Write(_typeIdConversion.GetTypeID<TObject>());
            method.Invoke(serialization, new object[] {replicatingObject, packet.OutputStream});
            packet.Close();

            return packet;
        }
    }
}