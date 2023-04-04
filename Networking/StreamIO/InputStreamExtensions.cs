using System.Numerics;

namespace Networking.StreamIO
{
    public static class IOStreamExtensions
    {
        public static Vector3 ReadVector3(this IInputStream inputStream) =>
            new(inputStream.ReadSingle(), inputStream.ReadSingle(), inputStream.ReadSingle());

        public static void Write(this IOutputStream outputStream, Vector3 vector3)
        {
            outputStream.Write(vector3.X);
            outputStream.Write(vector3.Y);
            outputStream.Write(vector3.Z);
        }
    }
}