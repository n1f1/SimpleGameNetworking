using System.IO;

namespace Networking.PacketSender
{
    public class WritingPacketData
    {
        public WritingPacketData(Stream stream, IOutputStream outputStream)
        {
            OutputStream = outputStream;
            Stream = stream;
        }

        public IOutputStream OutputStream { get; }
        public Stream Stream { get; }
        public byte[] Data { get; set; }
    }
}