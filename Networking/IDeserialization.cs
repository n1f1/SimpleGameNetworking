namespace Networking
{
    public interface IDeserialization<out T>
    {
        public T Deserialize(IInputStream inputStream);
    }
}