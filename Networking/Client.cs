using System;
using System.Net.Sockets;
using Networking.Packets;
using Networking.PacketSender;
using Networking.StreamIO;

namespace Networking
{
    public class Client
    {
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        public IInputStream InputStream { get; }
        private IOutputStream _outputStream;
        public INetworkPacketSender Sender { get; }

        public Client(TcpClient tcpClient, int id)
        {
            Id = id;
            _tcpClient = tcpClient ?? throw new ArgumentNullException(nameof(tcpClient));

            _networkStream = tcpClient.GetStream();
            InputStream = new BinaryReaderInputStream(_networkStream);
            _outputStream = new BinaryWriterOutputStream(_networkStream);
            Sender = new SendingPacketsDebug(new NetworkPacketSender(_outputStream));
        }

        public int Id { get; }
        public bool Disconnected => CheckDisconnected();

        public void Welcome()
        {
            PacketHeader packetHeader = new PacketHeader(PacketType.Handshake);
            MemoryNetworkPacket memoryNetworkPacket = new MemoryNetworkPacket(packetHeader);
            memoryNetworkPacket.OutputStream.Write(Id);
            memoryNetworkPacket.Close();
            Sender.SendPacket(memoryNetworkPacket);
        }

        private bool CheckDisconnected()
        {
            /*if (_tcpClient.Client.Poll(0, SelectMode.SelectWrite) && !_tcpClient.Client.Poll(0, SelectMode.SelectError))
            {
                byte[] buff = new byte[1];

                try
                {
                    if (_tcpClient.Client.Receive(buff, SocketFlags.Peek) == 0)
                        return true;
                }
                catch
                {
                    return true;
                }
            }*/

            return false;
        }
    }
}