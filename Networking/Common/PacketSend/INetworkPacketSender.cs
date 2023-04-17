namespace Networking.Common.PacketSend
{
    public interface INetworkPacketSender
    {
        void SendPacket(INetworkPacket networkPacket);
    }
}