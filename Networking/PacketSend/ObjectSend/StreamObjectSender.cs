using Networking.PacketReceive.Replication.ObjectCreationReplication;
using Networking.StreamIO;

namespace Networking.PacketSend.ObjectSend
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