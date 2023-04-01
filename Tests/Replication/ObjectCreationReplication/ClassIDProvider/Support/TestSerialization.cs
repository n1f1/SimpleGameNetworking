using Networking.Replication.Serialization;
using Networking.StreamIO;

namespace Tests.Replication.ObjectCreationReplication.ClassIDProvider.Support
{
    public class TestSerialization<T> : ISerialization<T>
    {
        public bool Serialized;
        
        public void Serialize(T inObject, IOutputStream outputStream)
        {
            Serialized = true;
        }
    }
}