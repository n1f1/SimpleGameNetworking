using System;
using System.Collections.Generic;
using Networking.PacketReceive.Replication.ObjectCreationReplication;
using NUnit.Framework;

namespace Tests.Replication.ObjectCreationReplication.ClassIDProvider
{
    public class ClassIDProviderTests
    {
        [Test]
        public void CanGetAndRemoveByAddedID()
        {
            Dictionary<Type, int> dictionary = new Dictionary<Type, int>()
            {
                {typeof(object), 1},
                {typeof(int), 3}
            };
            
            TypeIdConversion typeIdConversion = new TypeIdConversion(dictionary);
            Assert.AreEqual(typeIdConversion.GetTypeID<object>(), 1);
            Assert.AreEqual(typeIdConversion.GetTypeID<int>(), 3);
            Assert.AreEqual(typeIdConversion.GetTypeByID(1), typeof(object));
            Assert.AreEqual(typeIdConversion.GetTypeByID(3), typeof(int));
        }

        [Test]
        public void CanNotAddSameID()
        {
            Dictionary<Type, int> dictionary = new Dictionary<Type, int>()
            {
                {typeof(object), 1},
                {typeof(int), 1}
            };

            Assert.Throws<ArgumentException>(() => new TypeIdConversion(dictionary));
        }
    }
}