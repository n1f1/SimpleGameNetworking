using System;
using System.IO;
using System.Net.Sockets;
using Networking.Common.Utilities;

namespace Networking.Common.StreamIO
{
    public class BinaryReaderInputStream : IInputStream
    {
        private readonly NetworkStream _networkStream;
        private readonly BinaryReader _binaryReader;

        public BinaryReaderInputStream(NetworkStream networkStream)
        {
            _networkStream = networkStream ?? throw new ArgumentNullException(nameof(networkStream));
            _binaryReader = new BinaryReader(networkStream);
        }

        public bool NotEmpty() =>
            _networkStream.DataAvailable;

        public int ReadInt32() =>
            _binaryReader.ReadInt32();

        public short ReadInt16() =>
            _binaryReader.ReadInt16();

        public string ReadString() =>
            _binaryReader.ReadString();

        public byte[] ReadBytes(int bytes) =>
            _binaryReader.ReadBytes(bytes);

        public float ReadSingle() =>
            _binaryReader.ReadSingle();

        public long ReadInt64() => 
            _binaryReader.ReadInt64();

        public byte[] ReadAll()
        {
            byte[] readBuffer = new byte[1500];
            using MemoryStream stream = new MemoryStream();

            do
            {
                int numberOfBytesRead = _networkStream.Read(readBuffer, 0, readBuffer.Length);
                stream.Write(readBuffer, 0, numberOfBytesRead);
            } while (_networkStream.DataAvailable);

            stream.Seek(0, SeekOrigin.Begin);

            return stream.ReadAll();
        }
    }
}