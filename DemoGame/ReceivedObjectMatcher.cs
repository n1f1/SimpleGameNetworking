using Networking;

namespace DemoGame
{
    public class ReceivedReplicatedObjectMatcher : IReplicatedObjectReceiver<object>
    {
        public void Receive(object createdObject)
        {
            if (createdObject is ICommand command)
                command.Execute();
        }
    }
}