using System;
using System.Collections.Generic;
using Networking.ObjectsHashing;
using Networking.Replication.ObjectCreationReplication;

namespace DemoGame.NetworkingTypesConfigurations
{
    public static class TypeToSerializationObject
    {
        public static IEnumerable<(Type, object)> Create(IHashedObjectsList objectsList, ITypeIdConversion typeId)
        {
            IEnumerable<(Type, object)> tuples = new List<(Type, object)>
            {
                (typeof(Player), new PlayerSerialization(objectsList, typeId)),
                (typeof(MoveCommand), new MoveCommandSerialization(objectsList, typeId))
            };

            return tuples;
        }
    }
}