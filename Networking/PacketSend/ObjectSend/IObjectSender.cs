namespace Networking.PacketSend.ObjectSend
{
    public interface INetworkObjectSender
    {
        void Send<TType>(TType sent);
    }
}