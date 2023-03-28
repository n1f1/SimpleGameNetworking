using System;
using System.Numerics;

namespace Networking
{
    public interface IOutputStream
    {
        bool NotEmpty();
        void Write(int data);
        void Write(short data);
        void Write(string write);
        void Write(Vector3 vector3);
        void Write(ReadOnlySpan<byte> bytes);
        void Close();
    }
}