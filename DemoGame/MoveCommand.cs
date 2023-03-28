using System;
using System.Numerics;

namespace DemoGame
{
    public class MoveCommand : ICommand
    {
        public MoveCommand(Movement movement, Vector3 moveDelta)
        {
            MoveDelta = moveDelta;
            Movement = movement ?? throw new ArgumentNullException(nameof(movement));
        }

        public Movement Movement { get; }
        public Vector3 MoveDelta { get; }

        public void Execute()
        {
            Movement.Move(MoveDelta);
        }
    }
}