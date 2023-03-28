using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Networking
{
    public class CreationReplicator
    {
        private readonly ITypeIdConversion _typeIdConversion;
        private readonly Dictionary<Type, IDeserialization<object>> _deserializationList;
        private readonly IReplicatedObjectReceiver<object> _replicatedObjectReceiver;

        public CreationReplicator(ITypeIdConversion typeIdConversion,
            Dictionary<Type, IDeserialization<object>> deserializationList, IReplicatedObjectReceiver<object> replicatedObjectReceiver)
        {
            _typeIdConversion = typeIdConversion;
            _deserializationList = deserializationList;
            _replicatedObjectReceiver = replicatedObjectReceiver;
        }

        public void Create(IInputStream inputStream)
        {
            int classId = inputStream.ReadInt32();
            Type creatingType = _typeIdConversion.GetTypeByID(classId);
            IDeserialization<object> deserialization = _deserializationList[creatingType];
            MethodInfo method = deserialization.GetType().GetMethod("Deserialize");
            object createdObject = method.Invoke(deserialization, new[] {inputStream});
            _replicatedObjectReceiver.Receive(createdObject);
        }
    }
}