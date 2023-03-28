namespace Networking
{
    public interface IReplicatedObjectReceiver<T>
    {
        void Receive(T createdObject);
    }
}