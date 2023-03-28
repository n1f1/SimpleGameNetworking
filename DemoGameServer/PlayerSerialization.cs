using System;
using DemoGame;
using DemoGame.ClassID;
using Networking;
using Networking.ObjectsHashing;

namespace Server
{
    public class PlayerSerialization : ISerialization<Player>
    {
        private readonly IHashedObjectsList _hashedObjects;
        private readonly IClassIDConfiguration _classID;

        public PlayerSerialization(IHashedObjectsList hashedObjects, IClassIDConfiguration classId)
        {
            _hashedObjects = hashedObjects;
            _classID = classId;
        }

        public void Serialize(Player inObject, IOutputStream outputStream)
        {
            outputStream.Write(_classID.GetClassID<Player>());
            outputStream.Write(inObject.Name);
            outputStream.Write(inObject.Points);
            Movement movement = inObject.Movement;
            outputStream.Write(movement.Position);
            outputStream.Write(_hashedObjects.Register(movement));
        }
    }
}