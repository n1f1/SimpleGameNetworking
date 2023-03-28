namespace Networking
{
    public interface ISerialization<T>
    {
        public void Serialize(T inObject, IOutputStream outputStream);
    }
}