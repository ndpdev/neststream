using System;
using System.IO;
using System.Net;
using System.Text;

namespace NestStream.Client
{
    public class NestStreamClient
    {
        private Uri _serverEndpoint;
        private string _accessToken;
        private NestStreamState _state;

        public NestStreamClient(string serverEndpoint, string accessToken)
        {
            _state = new NestStreamState(this);
            _serverEndpoint = new Uri(serverEndpoint);
            _accessToken = accessToken;
        }

        public async void Connect()
        {
            var client = WebRequest.Create(_serverEndpoint) as HttpWebRequest;
            client.Accept = "text/event-stream";
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {_accessToken}");
            client.AllowReadStreamBuffering = false;
            client.ReadWriteTimeout = 60 * 1000;

            string inputLine;
            using (var resp = client.GetResponse())
            using (var stream = resp.GetResponseStream())
            using (var reader = new StreamReader(stream, Encoding.UTF8, false))
            {
                while ((inputLine = await reader.ReadLineAsync()) != null)
                    _state.ProcessLine(inputLine);
            }
        }

        public event EventHandler<ReceivedNestStreamEventArgs> ReceivedNestStreamEvent;

        internal void RaiseNestStreamEvent(ReceivedNestStreamEventArgs args)
        {
            ReceivedNestStreamEvent.Invoke(this, args);
        }
    }
}