using Networking.Common.StreamIO;

namespace Networking.Common.Replication
{
    public interface ICreationPacketHandler
    {
        void Create(IInputStream inputStream);
    }
}