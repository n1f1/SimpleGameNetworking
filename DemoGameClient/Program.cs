using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DemoGame;
using DemoGame.ClassID;
using DemoGame.NetworkingTypesConfigurations;
using Networking.ObjectsHashing;
using Networking.Packets;
using Networking.PacketSender;
using Networking.Replication;
using Networking.Replication.ObjectCreationReplication;
using Networking.Replication.Serialization;
using Networking.StreamIO;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using TcpClient tcpClient = new TcpClient();

            Console.WriteLine("Try connect...");

            await tcpClient.ConnectAsync("192.168.1.87", 55555);

            Console.WriteLine("Connected...");

            NetworkStream networkStream = tcpClient.GetStream();
            IInputStream inputStream = new BinaryReaderInputStream(networkStream);
            IOutputStream outputStream = new BinaryWriterOutputStream(networkStream);

            IHashedObjectsList hashedObjects = new HashedObjectsList();

            ITypeIdConversion typeId = new TypeIdConversion(
                new Dictionary<Type, int>().PopulateDictionaryFromTuple(SerializableTypesIdMap.Get()));

            Dictionary<Type, IDeserialization<object>> deserialization = new();
            deserialization.PopulateDictionary(TypeToSerializationObject.Create(hashedObjects, typeId));
            Dictionary<Type, object> serialization = new Dictionary<Type, object>();
            serialization.PopulateDictionary(TypeToSerializationObject.Create(hashedObjects, typeId));

            INetworkPacketSender networkPacketSender = new SendingPacketsDebug(new NetworkPacketSender(outputStream));

            ObjectReplicationPacketFactory replicationPacketFactory =
                new ObjectReplicationPacketFactory(serialization, typeId);

            Game game = new Game(networkPacketSender, replicationPacketFactory);
            
            Dictionary<Type, object> receivers = new Dictionary<Type, object>
            {
                {typeof(MoveCommand), new CommandsReceiver()},
                {typeof(Player), new PlayerReceiver(game)}
            };

            Replicator replicator =
                new Replicator(new CreationReplicator(typeId, deserialization,
                    new ReceivedReplicatedObjectMatcher(receivers)));
            
            while (true)
            {
                ReceivePackets(inputStream, replicator);
                game.Update();
                Task.Yield();
            }
        }

        private static void ReceivePackets(IInputStream inputStream, Replicator replicator)
        {
            if (inputStream.NotEmpty() == false)
                return;

            int readInt32 = inputStream.ReadInt32();
            PacketType packetType = (PacketType) readInt32;

            Console.WriteLine("\nReceived " + packetType + " time: " + DateTime.Now.TimeOfDay);

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