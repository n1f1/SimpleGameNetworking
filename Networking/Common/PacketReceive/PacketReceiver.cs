using System;
using Networking.Common.Packets;
using Networking.Common.Replication;
using Networking.Common.StreamIO;

namespace Networking.Common.PacketReceive
{
    public class PacketReceiver : IPacketReceiver
    {
        public void ReceivePackets(IInputStream inputStream, IReplicationPacketRead replicationPacketRead)
        {
            while (inputStream.NotEmpty())
            {
                if (inputStream.NotEmpty() == false)
                    return;

                PacketType packetType = (PacketType) inputStream.ReadInt32();

                if (packetType == PacketType.ReplicationData)
                    replicationPacketRead.ProcessReplicationPacket(inputStream);
                else
                    throw new InvalidOperationException();
            }
        }
    }
}