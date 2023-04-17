using System;

namespace Networking.Common
{
    public interface IGenericInterfaceList
    {
        Type InterfaceType { get; }
        bool ContainsForType(Type type);
        object GetForType(Type type);
        void Register(Type type, object genericInterface);
    }
}