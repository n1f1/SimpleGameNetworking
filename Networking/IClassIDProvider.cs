using System;

namespace Networking
{
    public interface ITypeIdConversion
    {
        int GetTypeID<T>();
        Type GetTypeByID(int classId);
    }
}