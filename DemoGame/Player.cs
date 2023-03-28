using System.Numerics;

namespace DemoGame
{
    public class Player
    {
        public string Name;
        public int Points;
        public Movement Movement;

        public static Player Default() =>
            new Player() {Name = "DefaultPlayer", Points = 25, Movement = new Movement(Vector3.One)};
    }

    public class Movement
    {
        public Vector3 Position;

        public Movement(Vector3 position)
        {
            Position = position;
        }

        public void Move(Vector3 delta)
        {
            Position += delta;
        }
    }
}