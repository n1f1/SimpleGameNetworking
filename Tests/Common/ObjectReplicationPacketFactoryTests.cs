using System;
using System.Collections.Generic;
using Networking.Common;
using Networking.Common.PacketSend;
using Networking.Common.Replication.ObjectCreationReplication;
using Networking.Common.Replication.Serialization;
using NUnit.Framework;
using Tests.Common.Replication.ObjectCreationReplication.Support;
using Tests.Common.Support;

namespace Tests.Common
{
    public class ObjectReplicationPacketFactoryTests
    {
        [Test]
        public void ConNotInitializeWithInvalidList()
        {
            ITypeIdConversion idConversion = new NullIDConversion();
            Dictionary<Type, object> dictionary = new() {{typeof(int), new TestImplementation<int>()}};
            IGenericInterfaceList invalidList =
                new GenericInterfaceWithParameterList(dictionary, typeof(ITestGenericInterface<>));

            Assert.Throws<ArgumentException>(() => new ObjectReplicationPacketFactory(invalidList, idConversion));
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