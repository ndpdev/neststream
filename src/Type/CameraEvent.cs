using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace NestStream.Type
{
    public class CameraEvent
    {
        public bool HasSound { get; }
        public bool HasMotion { get; }
        public bool HasPerson { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public DateTime UrlsExpireTime { get; }
        public Uri WebUrl { get; }
        public Uri AppUrl { get; }
        public Uri ImageUrl { get; }
        public Uri AnimatedImageUrl { get; }
        public List<int> ActivityZoneIds { get; }

        internal CameraEvent(JToken obj)
        {
            if (obj["has_sound"] != null) { HasSound = obj.Value<bool>("has_sound"); }
            if (obj["has_motion"] != null) { HasMotion = obj.Value<bool>("has_motion"); }
            if (obj["has_person"] != null) { HasPerson = obj.Value<bool>("has_person"); }
            if (obj["start_time"] != null) { StartTime = obj.Value<DateTime>("start_time"); }
            if (obj["end_time"] != null) { EndTime = obj.Value<DateTime>("end_time"); }
            if (obj["urls_expire_time"] != null) { UrlsExpireTime = obj.Value<DateTime>("urls_expire_time"); }
            if (obj["web_url"] != null) { try { WebUrl = new Uri(obj.Value<string>("web_url")); } catch {}}
            if (obj["app_url"] != null) { try { AppUrl = new Uri(obj.Value<string>("app_url")); } catch {}}
            if (obj["image_url"] != null) { try { ImageUrl = new Uri(obj.Value<string>("image_url")); } catch {}}
            if (obj["animated_image_url"] != null) { try { AnimatedImageUrl = new Uri(obj.Value<string>("animated_image_url")); } catch {}}
            if (obj["activity_zone_ids"] != null)
            {
                var activityZoneIds = new List<int>();
                foreach (JToken item in obj["activity_zone_ids"]) { activityZoneIds.Add(item.Value<int>()); }
                ActivityZoneIds = activityZoneIds;
            }
        }

        public override string ToString()
        {
            return $"{{Time:{StartTime} Sound:{HasSound} Motion:{HasMotion} Person:{HasPerson}}}";
        }
    }
}