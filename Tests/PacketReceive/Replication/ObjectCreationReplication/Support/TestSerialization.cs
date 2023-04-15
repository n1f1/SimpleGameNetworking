using Networking.PacketReceive.Replication.Serialization;
using Networking.StreamIO;

namespace Tests.PacketReceive.Replication.ObjectCreationReplication.Support
{
    public class TestSerialization<T> : ISerialization<T>
    {
        public bool Serialized;
        
        public void Serialize(T inObject, IOutputStream outputStream)
        {
            Serialized = true;
            outputStream.Write(1);
        }
    }
}