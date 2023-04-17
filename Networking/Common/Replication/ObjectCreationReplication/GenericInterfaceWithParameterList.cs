using System;
using System.Collections.Generic;

namespace Networking.Common.Replication.ObjectCreationReplication
{
    public class GenericInterfaceWithParameterList : IGenericInterfaceList
    {
        private readonly Dictionary<Type, object> _serialization;
        private readonly Type _interfaceType;

        public GenericInterfaceWithParameterList(Dictionary<Type, object> serialization, Type interfaceType)
        {
            foreach (var keyValuePair in serialization)
            {
                Type value = keyValuePair.Value.GetType();
                Type key = keyValuePair.Key;
                ReflectionUtility.AssertImplementsGenericInterface(value, key, interfaceType);
            }

            _serialization = serialization;
            _interfaceType = interfaceType ?? throw new ArgumentNullException(nameof(interfaceType));
        }

        public bool ContainsForType(Type type) =>
            _serialization.ContainsKey(type);

        public object GetForType(Type type)
        {
            if (ContainsForType(type) == false)
                throw new InvalidOperationException($"{_interfaceType} for {type} was not registered");

            return _serialization[type];
        }

        public void Register(Type type, object genericInterface)
        {
            if (ReflectionUtility.AssertImplementsGenericInterface(genericInterface.GetType(), type, _interfaceType) == false)
                return;

            if (_serialization.ContainsKey(type))
                throw new InvalidOperationException($"Already contains {_interfaceType} for {type} type");

            _serialization.Add(type, genericInterface);
        }
    }
}