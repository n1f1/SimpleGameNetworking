using System;

namespace Networking.Common.Replication.ObjectCreationReplication
{
    public interface ITypeIdConversion
    {
        int GetTypeID<T>();
        Type GetTypeByID(int classId);
    }
}