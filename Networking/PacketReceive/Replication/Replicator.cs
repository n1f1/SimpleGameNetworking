using System;
using Networking.StreamIO;

namespace Networking.PacketReceive.Replication
{
    public class ReplicationPacketRead : IReplicationPacketRead
    {
        private readonly ICreationPacketHandler _creationReplicator;

        public ReplicationPacketRead(ICreationPacketHandler creationReplicator)
        {
            _creationReplicator = creationReplicator;
        }

        public void ProcessReplicationPacket(IInputStream inputStream)
        {
            ReplicationType replicationType = (ReplicationType) inputStream.ReadInt32();

            Console.WriteLine("Received " + replicationType);
            
            if (replicationType is ReplicationType.CreateObject)
                _creationReplicator.Create(inputStream);
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}