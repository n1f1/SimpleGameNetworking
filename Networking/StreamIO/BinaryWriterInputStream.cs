using System;
using System.IO;

namespace Networking.StreamIO
{
    public class BinaryWriterOutputStream : IOutputStream
    {
        private readonly Stream _networkStream;
        private readonly BinaryWriter _binaryWriter;
        private bool _closed;

        public BinaryWriterOutputStream(Stream networkStream)
        {
            _networkStream = networkStream ?? throw new ArgumentNullException(nameof(networkStream));
            _binaryWriter = new BinaryWriter(networkStream);
        }

        public bool Closed => _closed;

        public bool NotEmpty() =>
            _networkStream.Length != 0;

        public void Write(int data) => 
            _binaryWriter.Write(data);

        public void Write(long data) => 
            _binaryWriter.Write(data);

        public void Write(float data) => 
            _binaryWriter.Write(data);

        public void Write(short data) => 
            _binaryWriter.Write(data);

        public void Write(string write) => 
            _binaryWriter.Write(write);

        public void Write(ReadOnlySpan<byte> bytes) => 
            _binaryWriter.Write(bytes);

        public void Close()
        {
            _binaryWriter.Close();
            _closed = true;
        }
    }
}