namespace Networking.Common.PacketReceive
{
    public interface IReplicatedObjectReceiver<in T>
    {
        void Receive(T createdObject);
    }
}