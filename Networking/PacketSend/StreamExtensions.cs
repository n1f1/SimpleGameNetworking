using System.IO;

namespace Networking.PacketSend
{
    public static class StreamExtensions
    {
        public static byte[] ReadAll(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            int numBytesToRead = (int) stream.Length;
            int numBytesRead = 0;

            while (numBytesToRead > 0)
            {
                int n = stream.Read(bytes, numBytesRead, numBytesToRead);

                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }

            return bytes;
        }
    }
}