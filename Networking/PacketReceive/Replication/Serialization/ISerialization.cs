using Networking.StreamIO;

namespace Networking.PacketReceive.Replication.Serialization
{
    public interface ISerialization<in T>
    {
        public void Serialize(T inObject, IOutputStream outputStream);
    }
}