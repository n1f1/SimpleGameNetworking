using System;
using Networking.Replication.ObjectCreationReplication;

namespace Tests.Replication.ObjectCreationReplication.ClassIDProvider.Support
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