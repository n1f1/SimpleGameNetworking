using Networking.StreamIO;

namespace Networking.PacketReceive.Replication.Serialization
{
    public interface IDeserialization<out T>
    {
        public T Deserialize(IInputStream inputStream);
    }
}