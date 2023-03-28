using System;
using System.Collections.Generic;

namespace Networking
{
    public class CreationReplicator
    {
        private readonly Dictionary<int, IFactory> _factoryDictionary;

        public CreationReplicator(Dictionary<int, IFactory> factoryDictionary)
        {
            _factoryDictionary = factoryDictionary ?? throw new ArgumentNullException(nameof(factoryDictionary));
        }

        public void Create(IInputStream inputStream)
        {
            IFactory factory = _factoryDictionary[inputStream.ReadInt32()];
            Console.WriteLine(factory);
            factory.Create(inputStream);
        }
    }
}