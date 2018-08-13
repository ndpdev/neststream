using System;
using Newtonsoft.Json.Linq;

namespace NestStream.Type
{
    public class StructureWhere
    {
        public string WhereID { get; }
        public string Name { get; set; }

        internal StructureWhere(JToken obj)
        {
            if (obj["where_id"] != null) { WhereID = obj.Value<string>("where_id"); }
            if (obj["name"] != null) { Name = obj.Value<string>("name"); }
        }

        public override string ToString()
        {
            return $"[Where {Name}] ID:{WhereID}";
        }
    }
}