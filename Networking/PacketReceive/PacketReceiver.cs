using System;
using Networking.PacketReceive.Replication;
using Networking.PacketSend.Packets;
using Networking.StreamIO;

namespace Networking.PacketReceive
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
                Console.WriteLine("\nReceived " + packetType + " time: " + DateTime.Now.TimeOfDay);

                if (packetType == PacketType.ReplicationData)
                    replicationPacketRead.ProcessReplicationPacket(inputStream);
                else
                    throw new InvalidOperationException();
            }
        }
    }
}