using Networking.Common.StreamIO;

namespace Networking.Common.Packets
{
    public class PacketHeader : IPacketHeader
    {
        private readonly PacketType _type;

        public PacketHeader(PacketType type)
        {
            _type = type;
        }

        public virtual void WriteHeader(IOutputStream outputStream)
        {
            outputStream.Write((int) _type);
        }
    }
}