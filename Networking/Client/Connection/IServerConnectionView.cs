namespace Networking.Client.Connection
{
    public interface IServerConnectionView
    {
        void DisplayConnecting();
        void DisplayError(string errorMessage);
        void DisplayConnected();
    }
}