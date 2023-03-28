using System;
using DemoGame;
using DemoGame.ClassID;
using Networking;
using Networking.ObjectsHashing;

namespace Server
{
    internal class MoveCommandSerialization : ISerialization<MoveCommand>
    {
        private readonly IHashedObjectsList _hashedObjects;
        private readonly IClassIDConfiguration _classIdConfiguration;

        public MoveCommandSerialization(IHashedObjectsList hashedObjects, IClassIDConfiguration configuration)
        {
            _classIdConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _hashedObjects = hashedObjects ?? throw new ArgumentNullException(nameof(hashedObjects));
        }

        public void Serialize(MoveCommand inObject, IOutputStream outputStream)
        {
            short movementId = _hashedObjects.GetID(inObject.Movement);
            outputStream.Write(_classIdConfiguration.GetClassID<MoveCommand>());
            outputStream.Write(movementId);
            outputStream.Write(inObject.MoveDelta);
        }
    }
}