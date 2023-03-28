using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using DemoGame;
using DemoGame.ClassID;
using Networking;
using Networking.ObjectsHashing;

namespace Client
{
    class Program
    {
        private static IClassIDConfiguration _classId;

        static async Task Main(string[] args)
        {
            using TcpClient tcpClient = new TcpClient();

            Console.WriteLine("Try connect...");

            await tcpClient.ConnectAsync("192.168.1.87", 55555);

            Console.WriteLine("Connected...");

            NetworkStream networkStream = tcpClient.GetStream();
            IInputStream inputStream = new BinaryReaderInputStream(networkStream);
            IOutputStream outputStream = new BinaryWriterOutputStream(networkStream);

            IHashedObjectsList objectsList = new HashedObjectsList();

            ClassIdConfiguration classId = new ClassIdConfiguration();
            classId.Initialize();
            _classId = classId;
            
            Dictionary<int, IFactory> factories = new Dictionary<int, IFactory>();
            factories.Add(_classId.GetClassID<MoveCommand>(), new MoveCommandFactory(objectsList));
            factories.Add(_classId.GetClassID<Player>(), new PlayerFactory(objectsList));

            Replicator replicator = new Replicator(new CreationReplicator(factories));

            while (true)
            {
                //string line = Console.ReadLine();

                while (inputStream.NotEmpty() == false)
                    Task.Yield();

                int readInt32 = inputStream.ReadInt32();
                Console.WriteLine(readInt32);
                PacketType packetType = (PacketType) readInt32;

                Console.WriteLine("Recieved " + packetType);

                if (packetType == PacketType.ReplicationData)
                    replicator.ProcessReplicationPacket(inputStream);
                else
                {
                    throw new InvalidOperationException();
                }

                Console.WriteLine("Receive " + packetType);
            }
        }
    }
}