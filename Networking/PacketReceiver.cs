using System;
using Networking.Packets;
using Networking.Replication;
using Networking.StreamIO;

namespace Networking
{
    public class PacketReceiver : IPacketReceiver
    {
        private IReplicationPacketRead _replicationPacketRead;

        public PacketReceiver(IReplicationPacketRead replicationPacketRead)
        {
            _replicationPacketRead = replicationPacketRead ?? throw new ArgumentNullException(nameof(replicationPacketRead));
        }

        public void ReceivePackets(IInputStream inputStream, IReplicationPacketRead replicationPacketRead)
        {
            if (inputStream.NotEmpty() == false)
                return;

            PacketType packetType = (PacketType) inputStream.ReadInt32();
            Console.WriteLine("\nReceived " + packetType + " time: " + DateTime.Now.TimeOfDay);

            if (packetType == PacketType.ReplicationData)
                replicationPacketRead.ProcessReplicationPacket(inputStream);
            else
                throw new InvalidOperationException();
        }
    }
}