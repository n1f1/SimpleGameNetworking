using System;
using Networking;

namespace DemoGame
{
    public class PlayerReceiver : IReplicatedObjectReceiver<Player>
    {
        private Game _game;
        private bool _received;

        public PlayerReceiver(Game game)
        {
            _game = game;
        }

        public void Receive(Player player)
        {
            if(_received)
                return;
            
            _received = true;
            Console.WriteLine("Receive " + player + "!");
            _game.Add(player);
        }
    }
}