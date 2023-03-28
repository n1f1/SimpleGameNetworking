using System;
using System.Collections.Generic;

namespace Networking.Replication.Serialization
{
    public static class FillSerializationDictionaryExtension
    {
        public static void Populate(this IDictionary<Type, object> dictionary,
            List<(Type, object)> tuples)
        {
            foreach ((Type, object) tuple in tuples)
                dictionary.Add(tuple.Item1, tuple.Item2);
        }

        public static void Populate(this IDictionary<Type, IDeserialization<object>> dictionary,
            List<(Type, object)> tuples)
        {
            foreach ((Type, object) tuple in tuples)
                dictionary.Add(tuple.Item1, (IDeserialization<object>) tuple.Item2);
        }
    }
}