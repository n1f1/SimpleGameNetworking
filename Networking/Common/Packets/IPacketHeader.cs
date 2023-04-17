using Networking.Common.StreamIO;

namespace Networking.Common.Packets
{
    public interface IPacketHeader
    {
        void WriteHeader(IOutputStream outputStream);
    }
}