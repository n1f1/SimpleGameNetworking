using System;
using DemoGame;
using Networking;
using Networking.ObjectsHashing;

namespace Client
{
    internal class PlayerFactory : IFactory, IDeserialization<Player>
    {
        private Player _player;
        private readonly IHashedObjectsList _movement;

        public PlayerFactory(IHashedObjectsList movement)
        {
            _movement = movement;
        }

        public void Create(IInputStream inputStream)
        {
            _player = Deserialize(inputStream);
        }

        public Player Deserialize(IInputStream inputStream)
        {
            Player player = new Player()
            {
                Name = inputStream.ReadString(),
                Points = inputStream.ReadInt32(),
            };

            Console.WriteLine(player.Name);
            player.Movement = new Movement(inputStream.ReadVector3());
            _movement.Register(player.Movement, inputStream.ReadInt16());
            Console.WriteLine(player.Movement.Position);

            return player;
        }
    }
}