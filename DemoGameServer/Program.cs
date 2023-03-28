using System;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Threading.Tasks;
using DemoGame;
using DemoGame.ClassID;
using Networking;
using Networking.ObjectsHashing;
using Networking.Packets;
using Networking.PacketSender;

namespace Server
{
    class Program
    {
        private static IClassIDConfiguration _classID;

        static async Task Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 55555);
            tcpListener.Start();
            Console.WriteLine("Listening for clients...");

            using TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

            Console.WriteLine("DemoGameClient connected...");

            NetworkStream networkStream = tcpClient.GetStream();
            IInputStream inputStream = new BinaryReaderInputStream(networkStream);
            IOutputStream outputStream = new BinaryWriterOutputStream(networkStream);


            Player player = Player.Default();
            IHashedObjectsList hashedObjects = new HashedObjectsList();

            ClassIdConfiguration classID = new ClassIdConfiguration();
            classID.Initialize();
            _classID = classID;

            INetworkPacketSender networkPacketSender = new SendingPacketsDebug(new NetworkPacketSender(outputStream));

            ReplicationPacketHeader packetHeader = new ReplicationPacketHeader(ReplicationType.CreateObject);
            NetworkPacket playerPacket = new NetworkPacket(packetHeader);
            PlayerSerialization playerSerialization = new PlayerSerialization(hashedObjects, _classID);
            playerSerialization.Serialize(player, playerPacket.OutputStream);
            playerPacket.Close();
            networkPacketSender.SendPacket(playerPacket);

            IPacketHeader move = new ReplicationPacketHeader(ReplicationType.CreateObject);
            MoveCommand moveCommand = new MoveCommand(player.Movement, Vector3.One);
            NetworkPacket moveCommandPacket = new NetworkPacket(move);
            MoveCommandSerialization moveCommandSerialization = new MoveCommandSerialization(hashedObjects, _classID);
            moveCommandSerialization.Serialize(moveCommand, moveCommandPacket.OutputStream);
            moveCommandPacket.Close();
            networkPacketSender.SendPacket(moveCommandPacket);

            while (true)
            {
                Console.WriteLine("Receiving...");

                while (inputStream.NotEmpty() == false)
                    Task.Yield();

                int read = inputStream.ReadInt32();

                Console.WriteLine("Received " + read);
            }
        }
    }
}