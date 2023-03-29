using System;
using System.Numerics;
using Networking.PacketSender;
using Networking.Replication.ObjectCreationReplication;

namespace DemoGame
{
    public class Game
    {
        private Player _player;
        private readonly INetworkPacketSender _networkPacketSender;
        private readonly ObjectReplicationPacketFactory _replicationPacketFactory;

        public Game(INetworkPacketSender networkPacketSender, ObjectReplicationPacketFactory replicationPacketFactory)
        {
            _replicationPacketFactory = replicationPacketFactory;
            _networkPacketSender = networkPacketSender;
        }

        public void Add(Player player)
        {
            _player = player;
        }

        public void Update()
        {
            if (_player != null && Console.KeyAvailable)
            {
                Console.WriteLine("send command");
                Console.ReadLine();
                MoveCommand command = new MoveCommand(_player.Movement, Vector3.One);
                INetworkPacket packet = _replicationPacketFactory.Create(command);
                _networkPacketSender.SendPacket(packet);
            }
        }
    }
}