using Networking;
using Networking.ObjectsHashing;
using Networking.Replication.ObjectCreationReplication;
using Networking.Replication.Serialization;
using Networking.StreamIO;

namespace DemoGame
{
    public class PlayerSerialization : DefaultSerialization, ISerialization<Player>, IDeserialization<Player>
    {
        public PlayerSerialization(IHashedObjectsList hashedObjects, ITypeIdConversion typeId) :
            base(hashedObjects, typeId)
        {
        }

        public void Serialize(Player inObject, IOutputStream outputStream)
        {
            Movement movement = inObject.Movement;
            outputStream.Write(inObject.Name);
            outputStream.Write(inObject.Points);
            outputStream.Write(movement.Position);
            outputStream.Write(HashedObjects.Register(movement));
        }

        public Player Deserialize(IInputStream inputStream)
        {
            Player player = new Player
            {
                Name = inputStream.ReadString(),
                Points = inputStream.ReadInt32(),
                Movement = new Movement(inputStream.ReadVector3())
            };

            HashedObjects.Register(player.Movement, inputStream.ReadInt16());

            return player;
        }
    }
}