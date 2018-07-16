using System;

namespace NestStream.Client
{
    public class ReceivedNestStreamEventArgs : EventArgs
    {
        public DateTime Timestamp { get; }
        public string Type { get; }
        public string Data { get; }

        public ReceivedNestStreamEventArgs(DateTime timestamp, string type, string data)
        {
            Timestamp = timestamp;
            Type = type;
            Data = data;
        }
    }
}