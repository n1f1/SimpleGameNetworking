namespace Networking.PacketSend
{
    public interface INetworkPacketSender
    {
        void SendPacket(INetworkPacket networkPacket);
    }
}