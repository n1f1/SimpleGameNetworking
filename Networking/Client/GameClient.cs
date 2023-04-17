using System;
using Networking.Common.PacketReceive;
using Networking.Common.Packets;
using Networking.Common.Replication;
using Networking.Common.StreamIO;

namespace Networking.Client
{
    public class GameClient : INetworkStreamRead
    {
        private readonly IReplicationPacketRead _packetRead;
        private int _id;

        public GameClient(IReplicationPacketRead packetRead)
        {
            _packetRead = packetRead ?? throw new ArgumentNullException(nameof(packetRead));
        }

        public void ReadNetworkStream(IInputStream inputStream)
        {
            while (inputStream.NotEmpty())
            {
                int readInt32 = inputStream.ReadInt32();
                PacketType packetType = (PacketType) readInt32;

                switch (packetType)
                {
                    case PacketType.ReplicationData:
                        _packetRead.ProcessReplicationPacket(inputStream);
                        break;
                    case PacketType.Handshake:
                        GetHandshake(inputStream);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        private void GetHandshake(IInputStream inputStream)
        {
            _id = inputStream.ReadInt32();
        }
    }
}