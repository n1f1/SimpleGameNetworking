using System;
using Networking.StreamIO;

namespace Networking.Replication
{
    public class Replicator
    {
        private readonly CreationReplicator _creationReplicator;

        public Replicator(CreationReplicator creationReplicator)
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