using System;
using System.Collections.Generic;
using Networking;
using Networking.ObjectsHashing;

namespace DemoGame
{
    public static class TypeToSerializationObject
    {
        public static List<(Type, object)> Create(IHashedObjectsList objectsList, ITypeIdConversion typeId)
        {
            List<(Type, object)> tuples = new List<(Type, object)>
            {
                (typeof(Player), new PlayerSerialization(objectsList, typeId)),
                (typeof(MoveCommand), new MoveCommandSerialization(objectsList, typeId))
            };

            return tuples;
        }
    }
}