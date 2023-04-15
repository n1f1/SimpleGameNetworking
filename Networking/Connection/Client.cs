using System;
using System.Net.Sockets;
using Networking.PacketSend;
using Networking.PacketSend.Packets;
using Networking.StreamIO;

namespace Networking.Connection
{
    public class Client
    {
        private readonly TcpClient _tcpClient;
        private readonly NetworkStream _networkStream;
        private readonly IOutputStream _outputStream;

        public Client(TcpClient tcpClient, int id)
        {
            Id = id;
            _tcpClient = tcpClient ?? throw new ArgumentNullException(nameof(tcpClient));

            _networkStream = tcpClient.GetStream();
            InputStream = new BinaryReaderInputStream(_networkStream);
            _outputStream = new BinaryWriterOutputStream(_networkStream);
            Sender = new SendingPacketsDebug(new NetworkPacketSender(_outputStream));
        }

        public IInputStream InputStream { get; }
        public INetworkPacketSender Sender { get; }
        public int Id { get; }
        public bool IsConnected => _tcpClient.Client.IsConnected();

        public void Welcome()
        {
            PacketHeader packetHeader = new PacketHeader(PacketType.Handshake);
            MemoryNetworkPacket memoryNetworkPacket = new MemoryNetworkPacket(packetHeader);
            memoryNetworkPacket.OutputStream.Write(Id);
            memoryNetworkPacket.Close();
            Sender.SendPacket(memoryNetworkPacket);
        }
    }
}