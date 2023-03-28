using Networking.StreamIO;

namespace Networking.Packets
{
    public interface IPacketHeader
    {
        void WriteHeader(IOutputStream outputStream);
    }
}