using Networking.Common.PacketReceive;

namespace Tests.Common.PacketReceive.Support
{
    public class TestReceiver<T> : IReplicatedObjectReceiver<T>
    {
        public bool Received { get; private set; }

        public void Receive(T createdObject)
        {
            Received = true;
        }
    }
}