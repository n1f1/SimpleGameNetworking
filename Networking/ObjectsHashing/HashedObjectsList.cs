using System;
using System.Collections;

namespace Networking.ObjectsHashing
{
    public class HashedObjectsList : IHashedObjectsList
    {
        private readonly Hashtable _typeToHashtableHashtable = new();
        private short _id;

        public bool HasInstance<TType>(short instanceId)
        {
            TwoWayHashtable hashtable = GetTwoWayHashtable<TType>();
            return hashtable.IdToObject.Contains(instanceId);
        }

        public bool HasInstance<TType>(TType tObject)
        {
            TwoWayHashtable hashtable = GetTwoWayHashtable<TType>();
            return hashtable.ObjectToId.Contains(tObject);
        }

        public TType GetInstance<TType>(short instanceId)
        {
            if (HasInstance<TType>(instanceId) == false)
                throw new InvalidOperationException();

            TwoWayHashtable hashtable = GetTwoWayHashtable<TType>();
            return (TType) hashtable.IdToObject[instanceId];
        }

        public void RegisterWithID<TType>(TType tObject, short instanceID)
        {
            if (tObject == null)
                throw new ArgumentNullException(nameof(tObject));

            if (HasInstance(tObject))
                throw new InvalidOperationException();

            RegisterWithId(tObject, instanceID);
        }

        public short GetID<TType>(TType tObject)
        {
            TwoWayHashtable hashtable = GetTwoWayHashtable<TType>();

            return (short) hashtable.ObjectToId[tObject];
        }

        public short RegisterOrGetRegistered<TType>(TType tObject) =>
            HasInstance(tObject) ? GetID(tObject) : RegisterWithId(tObject, ++_id);

        private short RegisterWithId<TType>(TType tObject, short id)
        {
            TwoWayHashtable hashtable = GetTwoWayHashtable<TType>();

            hashtable.IdToObject.Add(id, tObject);
            hashtable.ObjectToId.Add(tObject, id);

            return id;
        }

        private TwoWayHashtable GetTwoWayHashtable<TType>()
        {
            TwoWayHashtable hashtable;

            if (_typeToHashtableHashtable.ContainsKey(typeof(TType)))
                hashtable = (TwoWayHashtable) _typeToHashtableHashtable[typeof(TType)];
            else
            {
                hashtable = new TwoWayHashtable {IdToObject = new(), ObjectToId = new()};
                _typeToHashtableHashtable.Add(typeof(TType), hashtable);
            }

            return hashtable;
        }

        private class TwoWayHashtable
        {
            public Hashtable IdToObject;
            public Hashtable ObjectToId;
        }
    }
}