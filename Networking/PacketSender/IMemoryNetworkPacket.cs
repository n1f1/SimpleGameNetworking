namespace Networking.PacketSender
{
    public interface INetworkPacket
    {
        bool Complete { get; }
        byte[] Data { get; }
    }
}