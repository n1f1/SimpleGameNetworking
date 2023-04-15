using System;
using System.Collections.Generic;
using Networking.PacketReceive.Replication.ObjectCreationReplication;
using Networking.PacketReceive.Replication.Serialization;
using Networking.PacketSend;
using NUnit.Framework;
using Tests.PacketReceive.Replication.ObjectCreationReplication.Support;

namespace Tests.PacketReceive.Replication.ObjectCreationReplication
{
    public class ObjectReplicationPacketFactoryTests
    {
        [Test]
        public void InvokesSerializationMethod()
        {
            TestSerialization<int> serialization = new TestSerialization<int>();
            Dictionary<Type, object> dictionary = new()
            {
                {typeof(int), serialization}
            };

            ObjectReplicationPacketFactory packetFactory =
                new ObjectReplicationPacketFactory(
                    new GenericInterfaceWithParameterList(dictionary, typeof(ISerialization<>)),
                    new NullIDConversion());

            packetFactory.Create(0);
            Assert.IsTrue(serialization.Serialized);
        }

        [Test]
        public void ReturnsCompletePacket()
        {
            Dictionary<Type, object> dictionary = new()
            {
                {typeof(int), new TestSerialization<int>()}
            };

            ObjectReplicationPacketFactory packetFactory =
                new ObjectReplicationPacketFactory(
                    new GenericInterfaceWithParameterList(dictionary, typeof(ISerialization<>)),
                    new NullIDConversion());

            INetworkPacket networkPacket = packetFactory.Create(0);
            Assert.IsTrue(networkPacket.Complete);
        }
    }
}