﻿using System.Net.Sockets;

namespace Networking
{
    public static class SocketExtensions
    {
        public static bool IsConnected(this Socket client)
        {
            bool blockingState = client.Blocking;

            try
            {
                byte[] tmp = new byte[1];

                client.Blocking = false;
                client.Send(tmp, 0, 0);
                return true;
            }
            catch (SocketException e)
            {
                if (e.NativeErrorCode.Equals(10035))
                    return true;
                else
                {
                    return false;
                }
            }
            finally
            {
                client.Blocking = blockingState;
            }
        }
    }
}