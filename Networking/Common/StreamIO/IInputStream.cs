namespace Networking.Common.StreamIO
{
    public interface IInputStream
    {
        bool NotEmpty();
        int ReadInt32();
        byte[] ReadBytes(int bytes);
        short ReadInt16();
        string ReadString();
        byte[] ReadAll();
        float ReadSingle();
        long ReadInt64();
    }
}