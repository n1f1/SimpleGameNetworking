using Networking.StreamIO;

namespace Networking.PacketSend.Packets
{
    public interface IPacketHeader
    {
        void WriteHeader(IOutputStream outputStream);
    }
}