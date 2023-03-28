using Networking.StreamIO;

namespace Networking.Replication.Serialization
{
    public interface ISerialization<in T>
    {
        public void Serialize(T inObject, IOutputStream outputStream);
    }
}