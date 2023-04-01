using System;

namespace Networking.Replication.ObjectCreationReplication
{
    public interface ITypeIdConversion
    {
        int GetTypeID<T>();
        Type GetTypeByID(int classId);
    }
}