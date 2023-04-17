namespace Networking.Server.Connection
{
    public interface IClientConnection
    {
        void Connect(ServerClient serverClient);
    }
}