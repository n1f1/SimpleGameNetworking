using System;
using Networking.Common.PacketSend;
using NUnit.Framework;
using Tests.Common.PacketSend.Support;

namespace Tests.Common.PacketSend
{
    public class MemoryNetworkPacketTest
    {
        [Test]
        public void CompletesPacketAndWritesHeader()
        {
            MemoryNetworkPacket packet = new MemoryNetworkPacket(new TestPacketHeader());

            Assert.DoesNotThrow(() => packet.Close());
            Assert.True(packet.Complete);
            Assert.True(packet.OutputStream.Closed);
            Console.WriteLine(packet.Data.Length);
            Assert.True(packet.Data.Length == 1);
            Assert.True(packet.Data[0] == 1);
        }
    }
}