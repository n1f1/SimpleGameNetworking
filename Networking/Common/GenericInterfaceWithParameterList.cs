using System;
using System.Collections.Generic;
using Networking.Common.Replication.ObjectCreationReplication;

namespace Networking.Common
{
    public class GenericInterfaceWithParameterList : IGenericInterfaceList
    {
        private readonly Dictionary<Type, object> _serialization;

        public Type InterfaceType { get; }

        public GenericInterfaceWithParameterList(Dictionary<Type, object> serialization, Type interfaceType)
        {
            foreach (var keyValuePair in serialization)
            {
                Type value = keyValuePair.Value.GetType();
                Type key = keyValuePair.Key;
                ReflectionUtility.AssertImplementsGenericInterface(value, key, interfaceType);
            }

            _serialization = serialization;
            InterfaceType = interfaceType ?? throw new ArgumentNullException(nameof(interfaceType));
        }

        public bool ContainsForType(Type type) =>
            _serialization.ContainsKey(type);

        public object GetForType(Type type)
        {
            if (ContainsForType(type) == false)
                throw new InvalidOperationException($"{InterfaceType} for {type} was not registered");

            return _serialization[type];
        }

        public void Register(Type type, object genericInterface)
        {
            if (ReflectionUtility.AssertImplementsGenericInterface(genericInterface.GetType(), type, InterfaceType) ==
                false)
                return;

            if (_serialization.ContainsKey(type))
                throw new InvalidOperationException($"Already contains {InterfaceType} for {type} type");

            _serialization.Add(type, genericInterface);
        }
    }
}