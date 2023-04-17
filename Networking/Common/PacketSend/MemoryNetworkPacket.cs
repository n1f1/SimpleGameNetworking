using System.IO;
using Networking.Common.Packets;
using Networking.Common.StreamIO;
using Networking.Common.Utilities;

namespace Networking.Common.PacketSend
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