using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace NestStream.Type
{
    public class Camera
    {
        public string DeviceID { get; }
        public string SoftwareVersion { get; }
        public string StructureID { get; }
        public string WhereID { get; }
        public string WhereName { get; }
        public string Name { get; }
        public string NameLong { get; }
        public bool IsOnline { get; }
        public bool IsStreaming { get; set; }
        public bool IsAudioInputEnabled { get; }
        public DateTime LastIsOnlineChange { get; }
        public bool IsVideoHistoryEnabled { get; }
        public Uri WebUrl { get; }
        public Uri AppUrl { get; }
        public bool? IsPublicShareEnabled { get; }
        public List<CameraActivityZone> ActivityZones { get; }
        public Uri PublicShareUrl { get; }
        public Uri SnapshotUrl { get; }
        public CameraEvent LastEvent { get; }

        internal Camera(JToken obj)
        {
            if (obj["device_id"] != null) { DeviceID = obj.Value<string>("device_id"); }
            if (obj["software_version"] != null) { SoftwareVersion = obj.Value<string>("software_version"); }
            if (obj["structure_id"] != null) { StructureID = obj.Value<string>("structure_id"); }
            if (obj["where_id"] != null) { WhereID = obj.Value<string>("where_id"); }
            if (obj["where_name"] != null) { WhereName = obj.Value<string>("where_name"); }
            if (obj["name"] != null) { Name = obj.Value<string>("name"); }
            if (obj["name_long"] != null) { NameLong = obj.Value<string>("name_long"); }
            if (obj["is_online"] != null) { IsOnline = obj.Value<bool>("is_online"); }
            if (obj["is_streaming"] != null) { IsStreaming = obj.Value<bool>("is_streaming"); }
            if (obj["is_audio_input_enabled"] != null) { IsAudioInputEnabled = obj.Value<bool>("is_audio_input_enabled"); }
            if (obj["last_id_online_change"] != null) { LastIsOnlineChange = obj.Value<DateTime>("last_is_online_change"); }
            if (obj["web_url"] != null) { WebUrl = new Uri(obj.Value<string>("web_url")); }
            if (obj["app_url"] != null) { AppUrl = new Uri(obj.Value<string>("app_url")); }
            if (obj["is_public_share_enabled"] != null) { IsPublicShareEnabled = obj.Value<bool>("is_public_share_enabled"); }
            if (obj["activity_zones"] != null)
            {
                var activityZones = new List<CameraActivityZone>();
                foreach (var item in obj["activity_zones"]) { activityZones.Add(new CameraActivityZone(item)); }
                ActivityZones = activityZones;
            }
            if (obj["public_share_url"] != null) { try { PublicShareUrl = new Uri(obj.Value<string>("public_share_url")); } catch {}}
            if (obj["snapshot_url"] != null) { try { SnapshotUrl = new Uri(obj.Value<string>("snapshot_url")); } catch {}}
            if (obj["last_event"] != null) { LastEvent = new CameraEvent(obj["last_event"]); }
        }

        public override string ToString()
        {
            return $"[Camera {Name}] ID:{DeviceID} Online:{IsOnline} LastEvent:{LastEvent}";
        }
    }
}