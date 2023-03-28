using Networking.StreamIO;

namespace Networking.Replication.Serialization
{
    public interface IDeserialization<out T>
    {
        public T Deserialize(IInputStream inputStream);
    }
}