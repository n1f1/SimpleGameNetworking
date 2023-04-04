using System;

namespace Networking.StreamIO
{
    internal class TestLatencyPacket
    {
        public TestLatencyPacket(byte[] data, DateTime receiveTime)
        {
            ReceiveTime = receiveTime;
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public byte[] Data { get; }
        public DateTime ReceiveTime { get; }
    }
}