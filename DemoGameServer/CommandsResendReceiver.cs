using System;
using System.Reflection;
using DemoGame;
using Networking;
using Networking.PacketSender;
using Networking.Replication.ObjectCreationReplication;

namespace Server
{
    internal class CommandsResendReceiver : IReplicatedObjectReceiver<ICommand>
    {
        private readonly INetworkPacketSender _networkPacketSender;
        private readonly ObjectReplicationPacketFactory _replicationPacketFactory;

        public CommandsResendReceiver(INetworkPacketSender networkPacketSender,
            ObjectReplicationPacketFactory replicationPacketFactory)
        {
            _replicationPacketFactory = replicationPacketFactory;
            _networkPacketSender = networkPacketSender;
        }

        public void Receive(ICommand command)
        {
            Type replicatingObject = command.GetType();
            Type factoryType = _replicationPacketFactory.GetType();
            MethodInfo methodInfo = factoryType.GetMethod(nameof(_replicationPacketFactory.Create))
                .MakeGenericMethod(replicatingObject);
            INetworkPacket packet = (INetworkPacket) methodInfo.Invoke(_replicationPacketFactory, new object[] {command});
            _networkPacketSender.SendPacket(packet);
        }
    }
}