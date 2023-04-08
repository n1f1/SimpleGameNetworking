using System;
using System.Collections.Generic;

namespace Networking.PacketReceive.Replication.Serialization
{
    public static class FillSerializationDictionaryExtension
    {
        public static void PopulateDictionary(this IDictionary<Type, object> dictionary,
            IEnumerable<(Type, object)> tuples)
        {
            foreach ((Type, object) tuple in tuples)
                dictionary.Add(tuple.Item1, tuple.Item2);
        }

        public static void PopulateDictionary(this IDictionary<Type, IDeserialization<object>> dictionary,
            IEnumerable<(Type, object)> tuples)
        {
            foreach ((Type, object) tuple in tuples)
                dictionary.Add(tuple.Item1, (IDeserialization<object>) tuple.Item2);
        }
        
        public static Dictionary<Type, TKey> PopulateDictionaryFromTuple<TKey>(this Dictionary<Type, TKey> dictionary,
            IEnumerable<(Type, TKey)> tuples)
        {
            foreach ((Type, TKey) tuple in tuples)
                dictionary.Add(tuple.Item1, tuple.Item2);

            return dictionary;
        }
    }
}