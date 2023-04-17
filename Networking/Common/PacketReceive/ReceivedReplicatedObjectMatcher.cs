using System;
using System.Reflection;
using Networking.Common.Replication.ObjectCreationReplication;

namespace Networking.Common.PacketReceive
{
    public class ReceivedReplicatedObjectMatcher : IReplicatedObjectReceiver<object>
    {
        private readonly IGenericInterfaceList _receivers;
        private readonly Type _type = typeof(IReplicatedObjectReceiver<>);

        public ReceivedReplicatedObjectMatcher(IGenericInterfaceList receivers)
        {
            _receivers = receivers ?? throw new ArgumentNullException(nameof(receivers));

            if (_receivers.InterfaceType != _type)
                throw new ArgumentException($"{receivers} must have InterfaceType property of type {_type}");
        }

        public void Receive(object createdObject)
        {
            Type receivedObjectType = createdObject.GetType();
            object receiver = _receivers.GetForType(receivedObjectType);
            Type receiverType = receiver.GetType();
            MethodInfo method = receiverType.GetMethod(nameof(IReplicatedObjectReceiver<object>.Receive));
            method.Invoke(receiver, new[] {createdObject});
        }
    }
}