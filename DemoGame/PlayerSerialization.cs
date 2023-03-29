using System;
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

        public void Serialize(Player player, IOutputStream outputStream)
        {
            Movement movement = player.Movement;
            outputStream.Write(HashedObjects.Register(player));
            outputStream.Write(player.Name);
            outputStream.Write(player.Points);
            outputStream.Write(movement.Position);
            outputStream.Write(HashedObjects.Register(movement));
        }

        public Player Deserialize(IInputStream inputStream)
        {
            short playerInstanceId = inputStream.ReadInt16();
            
            Player player = new Player
            {
                Name = inputStream.ReadString(),
                Points = inputStream.ReadInt32(),
                Movement = new Movement(inputStream.ReadVector3())
            };

            HashedObjects.RegisterNew(player, playerInstanceId);
            HashedObjects.RegisterNew(player.Movement, inputStream.ReadInt16());
            Console.Write($" id: {playerInstanceId} position: {player.Movement.Position}");

            return player;
        }
    }
}