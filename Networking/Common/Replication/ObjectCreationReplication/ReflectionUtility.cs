using System;

namespace Networking.Common.Replication.ObjectCreationReplication
{
    public static class ReflectionUtility
    {
        public static bool AssertImplementsGenericInterface(Type implementation, Type parameter, Type genericInterface)
        {
            foreach (Type interfaceType in implementation.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericInterface &&
                    interfaceType.GetGenericArguments()[0] == parameter)
                    return true;
            }

            throw new ArgumentException(
                $"{implementation} must implement interface of type {genericInterface} with one generic parameter of type {parameter}");
        }
    }
}