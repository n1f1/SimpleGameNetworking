using System;
using System.Numerics;

namespace Networking.StreamIO
{
    public interface IOutputStream
    {
        bool NotEmpty();
        void Write(int data);
        void Write(short data);
        void Write(string write);
        void Write(float data);
        void Write(ReadOnlySpan<byte> bytes);
        void Close();
    }
}