using System;
using Newtonsoft.Json.Linq;

namespace NestStream.Type
{
    public class CameraActivityZone
    {
        public string Name { get; }
        public int ID { get; }

        internal CameraActivityZone(JToken obj)
        {
            if (obj["name"] != null) { Name = obj.Value<string>("name"); }
            if (obj["id"] != null) { ID = obj.Value<int>("id"); }
        }

        public override string ToString()
        {
            return $"[Zone {Name}] ID:{ID}";
        }
    }
}