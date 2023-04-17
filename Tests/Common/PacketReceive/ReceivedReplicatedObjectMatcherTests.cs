using System;
using System.Collections.Generic;
using Networking.Common;
using Networking.Common.PacketReceive;
using NUnit.Framework;
using Tests.Common.PacketReceive.Support;
using Tests.Common.Support;

namespace Tests.Common.PacketReceive
{
    public class ReceivedReplicatedObjectMatcherTests
    {
        [Test]
        public void InvokesReceiveMethod()
        {
            TestReceiver<int> receiver = new TestReceiver<int>();
            Dictionary<Type, object> dictionary = new() {{typeof(int), receiver}};

            ReceivedReplicatedObjectMatcher matcher =
                new ReceivedReplicatedObjectMatcher(
                    new GenericInterfaceWithParameterList(dictionary, typeof(IReplicatedObjectReceiver<>)));
            
            matcher.Receive(0);
            Assert.True(receiver.Received);
        }
        
        [Test]
        public void ConNotInitializeWithInvalidList()
        {
            Dictionary<Type, object> dictionary = new() {{typeof(int), new TestImplementation<int>()}};
            IGenericInterfaceList invalidList =
                new GenericInterfaceWithParameterList(dictionary, typeof(ITestGenericInterface<>));

            Assert.Throws<ArgumentException>(() => new ReceivedReplicatedObjectMatcher(invalidList));
        }
    }
}