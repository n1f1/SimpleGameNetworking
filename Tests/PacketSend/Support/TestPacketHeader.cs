using Networking.Common.Packets;
using Networking.Common.StreamIO;

namespace Tests.PacketSend.Support
{
    public class TestPacketHeader : IPacketHeader
    {
        public void WriteHeader(IOutputStream outputStream)
        {
            outputStream.Write(new byte[] {1});
        }
    }
}