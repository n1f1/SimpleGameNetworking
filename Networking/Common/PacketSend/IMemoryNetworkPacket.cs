namespace Networking.Common.PacketSend
{
    public interface INetworkPacket
    {
        bool Complete { get; }
        byte[] Data { get; }
    }
}