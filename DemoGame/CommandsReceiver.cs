using Networking;

namespace DemoGame
{
    public class CommandsReceiver : IReplicatedObjectReceiver<ICommand>
    {
        public void Receive(ICommand command)
        {
            command.Execute();
        }
    }
}