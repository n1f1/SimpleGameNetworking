using System;
using System.Collections.Generic;
using Networking.Common;
using NUnit.Framework;
using Tests.Common.Support;

namespace Tests.Common
{
    public class GenericInterfaceWithParameterListTest
    {
        [Test]
        public void CanNotInitializeWithoutValidInterface()
        {
            List<(Type, object)> dictionary = new()
            {
                (typeof(object), new TestImplementation<float>()),
                (typeof(float), new TestImplementation<object>()),
                (typeof(double), new object())
            };

            foreach ((Type, object) valueTuple in dictionary)
            {
                Dictionary<Type, object> serialization = new Dictionary<Type, object>()
                    {{valueTuple.Item1, valueTuple.Item2}};

                Assert.Throws<ArgumentException>(() =>
                    new GenericInterfaceWithParameterList(serialization, typeof(ITestGenericInterface<>)));
            }
        }

        [Test]
        public void CanInitializeWithValidObjects()
        {
            Dictionary<Type, object> dictionary = new()
            {
                {typeof(int), new TestImplementation<int>()},
                {typeof(float), new TestImplementation<float>()}
            };

            Assert.DoesNotThrow(() =>
                new GenericInterfaceWithParameterList(dictionary, typeof(ITestGenericInterface<>)));
        }

        [Test]
        public void CanRegisterWithValidInterface()
        {
            GenericInterfaceWithParameterList sut =
                new GenericInterfaceWithParameterList(new Dictionary<Type, object>(), typeof(ITestGenericInterface<>));

            Assert.DoesNotThrow(() => sut.Register(typeof(float), new TestImplementation<float>()));
            Assert.DoesNotThrow(() => sut.Register(typeof(int), new TestImplementation<int>()));
        }

        [Test]
        public void CanNotRegisterWithInvalidInterface()
        {
            GenericInterfaceWithParameterList sut =
                new GenericInterfaceWithParameterList(new Dictionary<Type, object>(), typeof(ITestGenericInterface<>));

            Assert.Throws<ArgumentException>(() =>
                sut.Register(typeof(object), new TestImplementation<float>()));
            Assert.Throws<ArgumentException>(() =>
                sut.Register(typeof(float), new TestImplementation<object>()));
            Assert.Throws<ArgumentException>(() =>
                sut.Register(typeof(double), new object()));
        }
        
        [Test]
        public void CanNotRegisterTwice()
        {
            GenericInterfaceWithParameterList sut =
                new GenericInterfaceWithParameterList(new Dictionary<Type, object>(), typeof(ITestGenericInterface<>));

            sut.Register(typeof(float), new TestImplementation<float>());
            
            Assert.Throws<InvalidOperationException>(() =>
                sut.Register(typeof(float), new TestImplementation<float>()));
        }
    }
}