using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DemoGame;
using DemoGame.ClassID;
using Networking;
using Networking.ObjectsHashing;

namespace Client
{
    class Program
    {
        private static ITypeIdConversion _typeId;

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

            TypeIdConversion typeId = new TypeIdConversion(
                new Dictionary<Type, int>
                {
                    {typeof(Player), BitConverter.ToInt32(Encoding.UTF8.GetBytes("PLYR"))},
                    {typeof(MoveCommand), BitConverter.ToInt32(Encoding.UTF8.GetBytes("CMVE"))}
                });
            _typeId = typeId;

            Dictionary<Type, IDeserialization<object>> deserialization =
                new Dictionary<Type, IDeserialization<object>>();

            deserialization.Populate(TypeToSerializationObject.Create(objectsList, _typeId));

            Replicator replicator =
                new Replicator(new CreationReplicator(_typeId, deserialization, new ReceivedReplicatedObjectMatcher()));

            while (true)
            {
                //string line = Console.ReadLine();

                while (inputStream.NotEmpty() == false)
                    Task.Yield();

                int readInt32 = inputStream.ReadInt32();
                Console.WriteLine(readInt32);
                PacketType packetType = (PacketType) readInt32;

                Console.WriteLine("Recieved " + packetType + " time: " + DateTime.Now.TimeOfDay);

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