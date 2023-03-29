using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using DemoGame;
using DemoGame.ClassID;
using DemoGame.NetworkingTypesConfigurations;
using Networking;
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
        private static int _id;
        private static Room _room = new();
        private static ObjectReplicationPacketFactory _replicationPacketFactory;
        private static List<Player> _players = new();

        static async Task Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 55555);
            tcpListener.Start();

            ITypeIdConversion typeId = new TypeIdConversion(
                new Dictionary<Type, int>().PopulateDictionaryFromTuple(SerializableTypesIdMap.Get()));

            Dictionary<Type, object> serialization = new Dictionary<Type, object>();
            IEnumerable<(Type, object)> typeToSerialization =
                TypeToSerializationObject.Create(new HashedObjectsList(), typeId);
            serialization.PopulateDictionaryFromTuple(typeToSerialization);

            _replicationPacketFactory = new ObjectReplicationPacketFactory(serialization, typeId);

            Dictionary<Type, IDeserialization<object>> deserialization = new();
            deserialization.PopulateDictionary(typeToSerialization);

            Dictionary<Type, object> receivers = new Dictionary<Type, object>
            {
                {
                    typeof(MoveCommand), new CompositeReceiver<ICommand>(
                        new CommandsReceiver(),
                        new CommandsResendReceiver(_room, _replicationPacketFactory))
                }
            };

            var replicator = new Replicator(new CreationReplicator(typeId, deserialization,
                new ReceivedReplicatedObjectMatcher(receivers)));

            while (true)
            {
                ListenNewClients(tcpListener);

                foreach (Client client in _room.Clients)
                    ReceivePackets(client.InputStream, replicator);
            }
        }

        private static void ListenNewClients(TcpListener tcpListener)
        {
            if (tcpListener.Pending())
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                tcpClient.NoDelay = true;
                tcpClient.SendBufferSize = 1500;
                Console.WriteLine("DemoGameClient connected...");
                Client client = new Client(tcpClient, ++_id);
                client.Welcome();
                _room.Add(client);
                AddClientToGame(client);
            }
        }

        private static void AddClientToGame(Client client)
        {
            Player player = Player.Default();

            INetworkPacket networkPacket = _replicationPacketFactory.Create(player);

            foreach (Client each in _room.Clients)
                each.Sender.SendPacket(networkPacket);

            foreach (Player other in _players)
                client.Sender.SendPacket(_replicationPacketFactory.Create(other));

            _players.Add(player);
        }

        private static void ReceivePackets(IInputStream inputStream, Replicator replicator)
        {
            if (inputStream.NotEmpty() == false)
                return;

            PacketType packetType = (PacketType) inputStream.ReadInt32();
            Console.WriteLine("\nReceived " + packetType + " time: " + DateTime.Now.TimeOfDay);

            if (packetType == PacketType.ReplicationData)
                replicator.ProcessReplicationPacket(inputStream);
            else
                throw new InvalidOperationException();
        }
    }
}