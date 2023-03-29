using System;
using Networking;

namespace DemoGame
{
    public class PlayerReceiver : IReplicatedObjectReceiver<Player>
    {
        private Game _game;

        public PlayerReceiver(Game game)
        {
            _game = game;
        }

        public void Receive(Player player)
        {
            Console.WriteLine("Receive " + player + "!");
            _game.Add(player);
        }
    }
}