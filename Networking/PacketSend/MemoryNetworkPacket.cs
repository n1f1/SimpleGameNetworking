using System.IO;
using Networking.PacketSend.Packets;
using Networking.StreamIO;

namespace Networking.PacketSend
{
    public class MemoryNetworkPacket : INetworkPacket
    {
        private readonly MemoryStream _stream;

        public MemoryNetworkPacket(IPacketHeader packetHeader)
        {
            _stream = new MemoryStream();
            OutputStream = new BinaryWriterOutputStream(_stream);
            packetHeader.WriteHeader(OutputStream);
        }

        public IOutputStream OutputStream { get; }
        public bool Complete { get; private set; }
        public byte[] Data { get; private set; }

        public void Close()
        {
            _stream.Seek(0, SeekOrigin.Begin);
            Data = _stream.ReadAll();
            OutputStream.Close();
            Complete = true;
        }
    }
}