using System.IO;
using Networking.PacketSend;

namespace Networking.StreamIO
{
    public class MemoryInputStream : IInputStream
    {
        private readonly BinaryReader _binaryReader;
        private readonly Stream _stream;

        public MemoryInputStream(Stream stream)
        {
            _stream = stream;
            _binaryReader = new BinaryReader(stream);
        }

        public bool NotEmpty() => 
            _stream.Length != _stream.Position;

        public int ReadInt32() =>
            _binaryReader.ReadInt32();

        public byte[] ReadBytes(int bytes) =>
            _binaryReader.ReadBytes(bytes);

        public short ReadInt16() =>
            _binaryReader.ReadInt16();

        public string ReadString() =>
            _binaryReader.ReadString();

        public byte[] ReadAll() =>
            _stream.ReadAll();

        public float ReadSingle() =>
            _binaryReader.ReadSingle();

        public long ReadInt64() => 
            _binaryReader.ReadInt64();
    }
}