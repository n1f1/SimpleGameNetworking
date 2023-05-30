namespace Networking.Common
{
    //TODO: remove constants and calculate RTT at runtime 
    public static class NetworkConstants
    {
        public const int BaseLatency = 100;
        public const int JitterDelta = 0;
        public static float RTT => 0.1f;
        public static float ServerFixedDeltaTime => 0.1f;
    }
}