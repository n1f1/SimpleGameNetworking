using Networking.Common.StreamIO;

namespace Networking.Common.Replication.Serialization
{
    public interface IDeserialization<out T>
    {
        public T Deserialize(IInputStream inputStream);
    }
}