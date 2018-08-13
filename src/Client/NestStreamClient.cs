using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NestStream.Client
{
    public class NestStreamClient
    {
        private Uri _serverEndpoint;
        private string _accessToken;
        private NestStreamState _state;
        private bool _isConnected;

        public NestStreamClient(string serverEndpoint, string accessToken)
        {
            _state = new NestStreamState(this);
            _serverEndpoint = new Uri(serverEndpoint);
            _accessToken = accessToken;
            _isConnected = false;
        }

        public async Task Connect()
        {
            int redirectLimit = 5;
            do
            {
                var client = WebRequest.Create(_serverEndpoint) as HttpWebRequest;
                client.Accept = "text/event-stream";
                client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {_accessToken}");
                client.AllowReadStreamBuffering = false;
                client.AllowAutoRedirect = false;
                client.ReadWriteTimeout = 60 * 1000;

                try
                {
                    string inputLine;
                    using (var resp = client.GetResponse())
                    using (var stream = resp.GetResponseStream())
                    using (var reader = new StreamReader(stream, Encoding.UTF8, false))
                    {
                        _isConnected = true;
                        while ((inputLine = await reader.ReadLineAsync()) != null) { _state.ProcessLine(inputLine); }
                        _isConnected = false;
                        break;
                    }
                }
                catch (WebException e)
                {
                    var errorResp = e.Response as HttpWebResponse;
                    if (errorResp.StatusCode == HttpStatusCode.RedirectKeepVerb)
                    {
                        Uri targetUri = new Uri(errorResp.Headers["Location"]);
                        _serverEndpoint = targetUri;
                        --redirectLimit;
                    }
                    else
                    {
                        _isConnected = false;
                        break;
                    }
                }
            }
            while (redirectLimit > 0);
        }

        public bool IsConnected { get => _isConnected; }
        public event EventHandler<ReceivedNestStreamEventArgs> ReceivedNestStreamEvent;

        internal void RaiseNestStreamEvent(ReceivedNestStreamEventArgs args)
        {
            ReceivedNestStreamEvent.Invoke(this, args);
        }
    }
}