using Networking.StreamIO;

namespace Networking
{
    public interface INetworkStreamRead
    {
        void ReadNetworkStream(IInputStream inputStream);
    }
}