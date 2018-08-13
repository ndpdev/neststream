using System;
using Newtonsoft.Json.Linq;

namespace NestStream.Type
{
    public class Metadata
    {
        public string AccessToken { get; }
        public int ClientVersion { get; }
        public string UserID { get; }

        internal Metadata(JToken obj)
        {
            if (obj["access_token"] != null) { AccessToken = obj.Value<string>("access_token"); }
            if (obj["client_version"] != null) { ClientVersion = obj.Value<int>("client_version"); }
            if (obj["user_id"] != null) { UserID = obj.Value<string>("user_id"); }
        }

        public override string ToString()
        {
            return $"[Metadata] User:{UserID} Version:{ClientVersion}";
        }
    }
}