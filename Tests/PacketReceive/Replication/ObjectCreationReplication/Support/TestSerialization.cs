using Networking.Common.Replication.Serialization;
using Networking.Common.StreamIO;

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