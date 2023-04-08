using Networking.StreamIO;

namespace Networking.PacketReceive
{
    public interface INetworkStreamRead
    {
        void ReadNetworkStream(IInputStream inputStream);
    }
}