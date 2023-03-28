using System.Numerics;

namespace Networking
{
    public interface IInputStream
    {
        bool NotEmpty();
        int ReadInt32();
        byte[] ReadBytes(int bytes);
        short ReadInt16();
        Vector3 ReadVector3();
        string ReadString();
    }
}