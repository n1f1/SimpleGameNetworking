using Networking.Common.StreamIO;

namespace Networking.Common.PacketReceive
{
    public interface INetworkStreamRead
    {
        void ReadNetworkStream(IInputStream inputStream);
    }
}