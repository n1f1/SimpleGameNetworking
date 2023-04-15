using Networking.PacketSend.Packets;
using Networking.StreamIO;

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