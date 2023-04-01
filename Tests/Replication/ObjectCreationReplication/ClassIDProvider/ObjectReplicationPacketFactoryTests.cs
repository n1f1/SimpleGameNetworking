using System;
using System.Collections.Generic;
using Networking.PacketSender;
using Networking.Replication.ObjectCreationReplication;
using NUnit.Framework;
using Tests.Replication.ObjectCreationReplication.ClassIDProvider.Support;

namespace Tests.Replication.ObjectCreationReplication.ClassIDProvider
{
    public class ObjectReplicationPacketFactoryTests
    {
        [Test]
        public void CanNotAddInvalidSerializationObject()
        {
            List<(Type, object)> dictionary = new()
            {
                (typeof(object), new TestSerialization<float>()),
                (typeof(float), new TestSerialization<object>()),
                (typeof(double), new object())
            };

            foreach ((Type, object) valueTuple in dictionary)
            {
                Dictionary<Type, object> serialization = new Dictionary<Type, object>()
                    {{valueTuple.Item1, valueTuple.Item2}};

                Assert.Throws<ArgumentException>(() =>
                    new ObjectReplicationPacketFactory(serialization, new NullIDConversion()));
            }
        }

        [Test]
        public void CanAddValidSerialization()
        {
            Dictionary<Type, object> dictionary = new()
            {
                {typeof(int), new TestSerialization<int>()}
            };

            Assert.DoesNotThrow(() =>
                new ObjectReplicationPacketFactory(dictionary, new NullIDConversion()));
        }

        [Test]
        public void InvokesSerializationMethod()
        {
            TestSerialization<int> serialization = new TestSerialization<int>();
            Dictionary<Type, object> dictionary = new()
            {
                {typeof(int), serialization}
            };

            ObjectReplicationPacketFactory packetFactory =
                new ObjectReplicationPacketFactory(dictionary, new NullIDConversion());

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
                new ObjectReplicationPacketFactory(dictionary, new NullIDConversion());

            INetworkPacket networkPacket = packetFactory.Create(0);
            Assert.IsTrue(networkPacket.Complete);
        }
    }
}