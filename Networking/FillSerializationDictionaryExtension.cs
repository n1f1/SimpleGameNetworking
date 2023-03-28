using System;
using System.Collections.Generic;

namespace Networking
{
    public static class FillSerializationDictionaryExtension
    {
        public static void Populate(this IDictionary<Type, ISerialization<object>> dictionary,
            List<(Type, object)> tuples)
        {
            foreach ((Type, object) tuple in tuples)
                dictionary.Add(tuple.Item1, (ISerialization<object>) tuple.Item2);
        }

        public static void Populate(this IDictionary<Type, IDeserialization<object>> dictionary,
            List<(Type, object)> tuples)
        {
            foreach ((Type, object) tuple in tuples)
                dictionary.Add(tuple.Item1, (IDeserialization<object>) tuple.Item2);
        }
    }
}