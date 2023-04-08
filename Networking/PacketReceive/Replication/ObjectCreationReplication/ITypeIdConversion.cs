using System;

namespace Networking.PacketReceive.Replication.ObjectCreationReplication
{
    public interface ITypeIdConversion
    {
        int GetTypeID<T>();
        Type GetTypeByID(int classId);
    }
}