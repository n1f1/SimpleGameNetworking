using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Networking.Client.Connection
{
    public class ServerConnection : IServerConnection
    {
        private readonly IServerConnectionView _serverConnectionView;
        private readonly string _host;
        private readonly int _port;
        private TcpClient _tcpClient;

        public ServerConnection(IServerConnectionView connectionView, string host, int port)
        {
            _serverConnectionView = connectionView ?? throw new ArgumentNullException(nameof(connectionView));
            _host = host ?? throw new ArgumentNullException(nameof(host));
            _port = port;
        }

        public TcpClient Client => _tcpClient;

        public async Task<bool> Connect()
        {
            TcpClient tcpClient = new TcpClient();
            _serverConnectionView.DisplayConnecting();

            try
            {
                await tcpClient.ConnectAsync(_host, _port);
            }
            catch (SocketException exception)
            {
                if (exception.ErrorCode.Equals(ConnectionErrors.ConnectionRefusedErrorCode))
                {
                    _serverConnectionView.DisplayError(ConnectionErrors.ServerIsUnavailableMessage);
                    return false;
                }

                _serverConnectionView.DisplayError(exception.ToString());
                return false;
            }
            catch (Exception)
            {
                _serverConnectionView.DisplayError(ConnectionErrors.UnknownErrorMessage);
                throw;
            }
            
            _serverConnectionView.DisplayConnected();
            _tcpClient = tcpClient;

            return true;
        }
    }
}