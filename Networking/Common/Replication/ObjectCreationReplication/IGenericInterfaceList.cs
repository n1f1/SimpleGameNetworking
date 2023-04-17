using System;

namespace Networking.Common.Replication.ObjectCreationReplication
{
    public interface IGenericInterfaceList
    {
        bool ContainsForType(Type type);
        object GetForType(Type type);
        void Register(Type type, object genericInterface);
    }
}