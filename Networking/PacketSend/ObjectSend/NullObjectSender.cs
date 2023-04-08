namespace Networking.PacketSend.ObjectSend
{
    public class NullNetworkObjectSender : INetworkObjectSender
    {
        public void Send<TType>(TType sent)
        {
        
        }
    }
}