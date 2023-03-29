using Networking.StreamIO;

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
            _networkOutputStream.Write(networkPacket.Data);
        }
    }
}