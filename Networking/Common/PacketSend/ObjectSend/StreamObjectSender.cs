using Networking.Common.Replication.ObjectCreationReplication;
using Networking.Common.StreamIO;

namespace Networking.Common.PacketSend.ObjectSend
{
    public class StreamObjectSender : INetworkObjectSender
    {
        private readonly NetworkObjectSender _sender;

        public StreamObjectSender(IOutputStream networkOutputStream,
            ObjectReplicationPacketFactory replicationPacketFactory)
        {
            INetworkPacketSender networkPacketSender =
                new SendingPacketsDebug(new NetworkPacketSender(networkOutputStream));

            _sender = new NetworkObjectSender(replicationPacketFactory, networkPacketSender);
        }

        public void Send<TType>(TType sent) =>
            _sender.Send(sent);
    }
}