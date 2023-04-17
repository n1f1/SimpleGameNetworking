namespace Networking.Common.PacketSend.ObjectSend
{
    public interface INetworkObjectSender
    {
        void Send<TType>(TType sent);
    }
}