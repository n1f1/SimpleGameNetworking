namespace Networking
{
    public partial interface ISerialization<T>
    {
        public void Serialize(T inObject, IOutputStream outputStream);
    }
}