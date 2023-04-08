using System;

namespace Networking.PacketReceive
{
    public class CompositeReceiver<TObject> : IReplicatedObjectReceiver<TObject>
    {
        private readonly IReplicatedObjectReceiver<TObject>[] _receivers;

        public CompositeReceiver(params IReplicatedObjectReceiver<TObject>[] receivers)
        {
            _receivers = receivers ?? throw new ArgumentNullException(nameof(receivers));
        }

        public void Receive(TObject createdObject)
        {
            foreach (IReplicatedObjectReceiver<TObject> receiver in _receivers) 
                receiver.Receive(createdObject);
        }
    }
}