namespace Networking.PacketSender
{
    public interface INetworkPacketSender
    {
        void SendPacket(INetworkPacket networkPacket);
    }
}