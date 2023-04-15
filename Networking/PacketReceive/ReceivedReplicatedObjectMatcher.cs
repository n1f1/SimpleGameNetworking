using System;
using System.Collections.Generic;
using System.Reflection;
using Networking.PacketReceive.Replication.ObjectCreationReplication;

namespace Networking.PacketReceive
{
    public class ReceivedReplicatedObjectMatcher : IReplicatedObjectReceiver<object>
    {
        private readonly IGenericInterfaceList _receivers;

        public ReceivedReplicatedObjectMatcher(IGenericInterfaceList receivers)
        {
            _receivers = receivers ?? throw new ArgumentNullException(nameof(receivers));
        }

        public void Receive(object createdObject)
        {
            Type receivedObjectType = createdObject.GetType();
            object receiver = _receivers.GetForType(receivedObjectType);
            Type receiverType = receiver.GetType();
            MethodInfo method = receiverType.GetMethod("Receive");
            method.Invoke(receiver, new[] {createdObject});
        }
    }
}