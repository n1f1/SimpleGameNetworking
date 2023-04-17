using System;
using Networking.Common.Replication.ObjectsHashing;
using NUnit.Framework;

namespace Tests.Common.Replication.ObjectsHashing
{
    public class HashedObjectsListTest
    {
        [Test]
        public void CanNotRegisterInvalidOrAlreadyRegistered()
        {
            HashedObjectsList list = new HashedObjectsList();
            list.RegisterOrGetRegistered(list);
            Assert.Throws<InvalidOperationException>(() => list.RegisterWithID(list, 0));
            Assert.Throws<ArgumentNullException>(() => list.RegisterWithID(default(object), 0));
            object newObject = new object();
            list.RegisterWithID(newObject, 0);
            Assert.Throws<InvalidOperationException>(() => list.RegisterWithID(newObject, 1));
        }
        
        [Test]
        public void ReturnsRightIdAndObject()
        {
            HashedObjectsList list = new HashedObjectsList();
            object newObject = new object();
            short id = list.RegisterOrGetRegistered(newObject);
            Assert.IsTrue(list.HasInstance(newObject));
            Assert.AreEqual(list.GetID(newObject), id);
            Assert.IsTrue(list.HasInstance<object>(id));
            Assert.AreEqual(list.GetInstance<object>(id), newObject);
        }
        
        [Test]
        public void RegistersOrGetsRegistered()
        {
            HashedObjectsList list = new HashedObjectsList();
            object newObject = new object();
            short id = list.RegisterOrGetRegistered(newObject);
            Assert.AreEqual(list.GetID(newObject), id);
            short getID = -1;
            Assert.DoesNotThrow(() => getID = list.RegisterOrGetRegistered(newObject));
            Assert.AreEqual(id, getID);
        }
    }
}