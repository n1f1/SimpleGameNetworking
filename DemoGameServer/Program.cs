using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
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

namespace Server
{
    class Program
    {
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
            IInputStream inputStream1 = new BinaryReaderInputStream(networkStream);

            ITypeIdConversion typeId = new TypeIdConversion(
                new Dictionary<Type, int>().PopulateDictionaryFromTuple(SerializableTypesIdMap.Get()));

            Dictionary<Type, object> serialization = new Dictionary<Type, object>();
            IEnumerable<(Type, object)> typeToSerialization = TypeToSerializationObject.Create(new HashedObjectsList(), typeId);
            serialization.PopulateDictionaryFromTuple(typeToSerialization);

            INetworkPacketSender networkPacketSender = new SendingPacketsDebug(new NetworkPacketSender(new BinaryWriterOutputStream(networkStream)));

            var replicationPacketFactory = new ObjectReplicationPacketFactory(serialization, typeId);


            Dictionary<Type, IDeserialization<object>> deserialization = new();
            deserialization.PopulateDictionary(typeToSerialization);

            Dictionary<Type, object> receivers = new Dictionary<Type, object>
            {
                {typeof(MoveCommand), new CommandsResendReceiver(networkPacketSender, replicationPacketFactory)},
            };

            var replicator = new Replicator(new CreationReplicator(typeId, deserialization,
                new ReceivedReplicatedObjectMatcher(receivers)));
            IInputStream inputStream = inputStream1;

            Player player = Player.Default();

            INetworkPacket networkPacket = replicationPacketFactory.Create(player);
            networkPacketSender.SendPacket(networkPacket);
            
            while (true)
            {
                ReceivePackets(inputStream, replicator);
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
        }
    }
}