using Networking.Common.Replication;
using Networking.Common.StreamIO;

namespace Networking.Common.Packets
{
    public class ReplicationPacketHeader : PacketHeader
    {
        private readonly ReplicationType _replicationType;

        public ReplicationPacketHeader(ReplicationType replicationType) : base(PacketType.ReplicationData)
        {
            _replicationType = replicationType;
        }

        public override void WriteHeader(IOutputStream outputStream)
        {
            base.WriteHeader(outputStream);
            outputStream.Write((int) _replicationType);
        }
    }
}