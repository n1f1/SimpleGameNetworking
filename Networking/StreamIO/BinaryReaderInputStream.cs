using System;
using System.IO;
using System.Net.Sockets;
using System.Numerics;

namespace Networking.StreamIO
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

        public Vector3 ReadVector3() =>
            new(_binaryReader.ReadSingle(), _binaryReader.ReadSingle(), _binaryReader.ReadSingle());

        public byte[] ReadBytes(int bytes) =>
            _binaryReader.ReadBytes(bytes);
    }
}