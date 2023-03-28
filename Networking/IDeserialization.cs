namespace Networking
{
    public partial interface IDeserialization<T>
    {
        public T Deserialize(IInputStream inputStream);
    }
}