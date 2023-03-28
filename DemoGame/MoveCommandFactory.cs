using System.Numerics;
using Networking;
using Networking.ObjectsHashing;

namespace DemoGame
{
    public class MoveCommandFactory : IDeserialization<MoveCommand>, IFactory
    {
        private readonly IHashedObjectsList _hashedObjects;

        public MoveCommandFactory(IHashedObjectsList hashedObjects)
        {
            _hashedObjects = hashedObjects;
        }

        public void Create(IInputStream inputStream)
        {
            Deserialize(inputStream).Execute();
        }

        public MoveCommand Deserialize(IInputStream inputStream)
        {
            short movementInstanceID = inputStream.ReadInt16();
            Movement movement = _hashedObjects.GetInstance<Movement>(movementInstanceID);
            Vector3 moveDelta = inputStream.ReadVector3();

            return new MoveCommand(movement, moveDelta);
        }
    }
}