namespace Networking.PacketReceive
{
    public interface IReplicatedObjectReceiver<in T>
    {
        void Receive(T createdObject);
    }
}