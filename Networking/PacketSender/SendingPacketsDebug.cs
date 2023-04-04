using System;

namespace Networking.PacketSender
{
    public class SendingPacketsDebug : INetworkPacketSender
    {
        private readonly INetworkPacketSender _networkPacketSender;

        public SendingPacketsDebug(INetworkPacketSender networkPacketSender)
        {
            _networkPacketSender = networkPacketSender ?? throw new ArgumentNullException(nameof(networkPacketSender));
        }

        public void SendPacket(INetworkPacket networkPacket)
        {
            _networkPacketSender.SendPacket(networkPacket);

            Console.Write("Send packet: ");

            foreach (byte b in networkPacket.Data)
                Console.Write(b + " ");

            Console.Write(" Size: " + networkPacket.Data.Length);
            Console.Write(" Time: " + DateTime.Now.TimeOfDay);
            Console.WriteLine();
        }
    }
}