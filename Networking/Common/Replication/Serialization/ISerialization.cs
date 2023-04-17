using Networking.Common.StreamIO;

namespace Networking.Common.Replication.Serialization
{
    public interface ISerialization<in T>
    {
        public void Serialize(T inObject, IOutputStream outputStream);
    }
}