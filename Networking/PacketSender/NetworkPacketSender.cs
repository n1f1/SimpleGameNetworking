using System;

namespace Networking.PacketSender
{
    public class NetworkPacketSender : INetworkPacketSender
    {
        private readonly IOutputStream _networkOutputStream;

        public NetworkPacketSender(IOutputStream networkOutputStream)
        {
            _networkOutputStream = networkOutputStream;
        }
        
        public void SendPacket(INetworkPacket networkPacket)
        {
            Console.Write(" Time: " + DateTime.Now.TimeOfDay);
            _networkOutputStream.Write(networkPacket.Data);
            Console.Write(" Time: " + DateTime.Now.TimeOfDay);
        }
    }
}