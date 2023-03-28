using System.IO;
using Networking;
using Networking.Packets;
using Networking.PacketSender;

namespace Server
{
    public class NetworkPacket : INetworkPacket
    {
        private readonly IPacketHeader _packetHeader;
        private readonly MemoryStream _stream;

        public NetworkPacket(IPacketHeader packetHeader)
        {
            _packetHeader = packetHeader;
            _stream = new MemoryStream();
            OutputStream = new BinaryWriterOutputStream(_stream);
            _packetHeader.WriteHeader(OutputStream);
        }

        public IOutputStream OutputStream { get; }
        public bool Complete { get; private set; }
        public byte[] Data { get; private set; }

        public void Close()
        {
            _stream.Seek(0, SeekOrigin.Begin);
            Data = _stream.ReadAll();
            _stream.Close();
            Complete = true;
        }
    }
}