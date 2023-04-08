namespace Networking.PacketSend
{
    public interface INetworkPacket
    {
        bool Complete { get; }
        byte[] Data { get; }
    }
}