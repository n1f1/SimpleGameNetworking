using System;
using System.Collections.Generic;
using System.IO;
using Networking.PacketReceive;
using Random = System.Random;

namespace Networking.StreamIO
{
    public class LatencyDebugTestNetworkStreamRead : INetworkStreamRead
    {
        private readonly INetworkStreamRead _networkStreamRead;
        private readonly LinkedList<TestLatencyPacket> _packets = new();
        private readonly Random _random;
        private readonly int _baseLatencyMilliseconds;
        private readonly int _jitterDeltaMilliseconds;

        public LatencyDebugTestNetworkStreamRead(INetworkStreamRead networkStreamRead, int baseLatencyMilliseconds,
            int jitterDeltaMilliseconds)
        {
            if (baseLatencyMilliseconds < 0)
                throw new ArgumentOutOfRangeException(nameof(baseLatencyMilliseconds));

            if (jitterDeltaMilliseconds < 0 || jitterDeltaMilliseconds > baseLatencyMilliseconds)
                throw new ArgumentOutOfRangeException(nameof(baseLatencyMilliseconds));

            _jitterDeltaMilliseconds = jitterDeltaMilliseconds;
            _baseLatencyMilliseconds = baseLatencyMilliseconds;
            _networkStreamRead = networkStreamRead ?? throw new ArgumentNullException(nameof(networkStreamRead));
            _random = new Random();
        }

        public void ReadNetworkStream(IInputStream inputStream)
        {
            if (inputStream.NotEmpty())
                AddToPacketQueue(inputStream);

            if (_packets.Count > 0)
                SendReadyPacket();
        }

        private void SendReadyPacket()
        {
            LinkedListNode<TestLatencyPacket> node = _packets.First;
            TestLatencyPacket packet = node.Value;

            if (packet.ReceiveTime < DateTime.Now)
            {
                _packets.Remove(node);
                MemoryStream memoryStream = new MemoryStream(packet.Data.Length);
                memoryStream.Write(packet.Data);
                memoryStream.Seek(0, SeekOrigin.Begin);
                _networkStreamRead.ReadNetworkStream(new MemoryInputStream(memoryStream));
            }
        }

        private void AddToPacketQueue(IInputStream inputStream) =>
            _packets.AddLast(new TestLatencyPacket(inputStream.ReadAll(), DateTime.Now + GetLatency()));

        private TimeSpan GetLatency()
        {
            int jitter = _random.Next() % _jitterDeltaMilliseconds;

            return TimeSpan.FromMilliseconds(_baseLatencyMilliseconds + jitter);
        }
    }
}