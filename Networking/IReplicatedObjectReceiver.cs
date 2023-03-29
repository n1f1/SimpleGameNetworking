namespace Networking
{
    public interface IReplicatedObjectReceiver<in T>
    {
        void Receive(T createdObject);
    }
}