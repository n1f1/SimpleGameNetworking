using System;
using Networking.Common.Replication.ObjectCreationReplication;

namespace Tests.PacketReceive.Replication.ObjectCreationReplication.Support
{
    public class NullIDConversion : ITypeIdConversion
    {
        public int GetTypeID<T>()
        {
            return 0;
        }

        public Type GetTypeByID(int classId)
        {
            return typeof(object);
        }
    }
}