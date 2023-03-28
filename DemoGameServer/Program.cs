using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
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
        private static ITypeIdConversion _typeId;

        static async Task Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 55555);
            tcpListener.Start();
            Console.WriteLine("Listening for clients...");

            using TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
            tcpClient.NoDelay = true;
            tcpClient.SendBufferSize = 1500;

            Console.WriteLine("DemoGameClient connected...");

            NetworkStream networkStream = tcpClient.GetStream();
            IInputStream inputStream = new BinaryReaderInputStream(networkStream);
            IOutputStream outputStream = new BinaryWriterOutputStream(networkStream);

            IHashedObjectsList hashedObjects = new HashedObjectsList();

            TypeIdConversion typeId = new TypeIdConversion( new Dictionary<Type, int>
            {
                {typeof(Player), BitConverter.ToInt32(Encoding.UTF8.GetBytes("PLYR"))},
                {typeof(MoveCommand), BitConverter.ToInt32(Encoding.UTF8.GetBytes("CMVE"))}
            });
            _typeId = typeId;

            INetworkPacketSender networkPacketSender = new SendingPacketsDebug(new NetworkPacketSender(outputStream));
            
            Player player = Player.Default();

            IPacketHeader packetHeader = new ReplicationPacketHeader(ReplicationType.CreateObject);
            MemoryNetworkPacket playerPacket = new MemoryNetworkPacket(packetHeader);
            PlayerSerialization playerSerialization = new PlayerSerialization(hashedObjects, _typeId);
            playerPacket.OutputStream.Write(_typeId.GetTypeID<Player>());
            playerSerialization.Serialize(player, playerPacket.OutputStream);
            playerPacket.Close();
            networkPacketSender.SendPacket(playerPacket);

            MoveCommand moveCommand = new MoveCommand(player.Movement, Vector3.One);
            
            IPacketHeader move = new ReplicationPacketHeader(ReplicationType.CreateObject);
            MemoryNetworkPacket moveCommandPacket = new MemoryNetworkPacket(move);
            MoveCommandSerialization moveCommandSerialization = new MoveCommandSerialization(hashedObjects, _typeId);
            moveCommandPacket.OutputStream.Write(_typeId.GetTypeID<MoveCommand>());
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