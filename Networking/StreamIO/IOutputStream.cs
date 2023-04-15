using System;

namespace Networking.StreamIO
{
    public interface IOutputStream
    {
        void Close();
        bool Closed { get; }
        bool NotEmpty();
        void Write(int data);
        void Write(long data);
        void Write(short data);
        void Write(string write);
        void Write(float data);
        void Write(ReadOnlySpan<byte> bytes);
    }
}