using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace NestStream.Type
{
    public class StreamEvent
    {
        private DateTime _timestamp;
        private string _path;
        private Metadata _metadata = null;
        private Dictionary<string, Thermostat> _thermostats = null;
        private Dictionary<string, SmokeAlarm> _smokeAlarms = null;
        private Dictionary<string, Camera> _cameras = null;
        private Dictionary<string, Structure> _structures = null;

        internal StreamEvent(string jsonInput)
        {
            var se = JObject.Parse(jsonInput);
            if (se["path"] != null)
            {
                _timestamp = DateTime.Now;
                _path = se.Value<string>("path");
                var data = se["data"];
                switch (_path)
                {
                    case "/": ParseRoot(data); break;
                    case "/metadata": ParseMetadata(data); break;
                    case "/devices": ParseDevices(data); break;
                    case "/devices/thermostats": ParseDevicesThermostats(data); break;
                    case "/devices/smoke_co_alarms": ParseDevicesSmokeAlarms(data); break;
                    case "/devices/cameras": ParseDevicesCameras(data); break;
                    case "/structures": ParseStructures(data); break;
                }
            }
        }

        private void ParseRoot(JToken obj)
        {
            var metadata = obj["metadata"];
            var devices = obj["devices"];
            var structures = obj["structures"];

            if (metadata != null) { ParseMetadata(metadata); }
            if (devices != null) { ParseDevices(devices); }
            if (structures != null) { ParseStructures(structures); }
        }

        private void ParseMetadata(JToken obj)
        {
            _metadata = new Metadata(obj);
        }

        private void ParseDevices(JToken obj)
        {
            var thermostats = obj["thermostats"];
            var smokeAlarms = obj["smoke_co_alarms"];
            var cameras = obj["cameras"];
            
            if (thermostats != null) { ParseDevicesThermostats(thermostats); }
            if (smokeAlarms != null) { ParseDevicesSmokeAlarms(smokeAlarms); }
            if (cameras != null) { ParseDevicesCameras(cameras); }
        }

        private void ParseDevicesThermostats(JToken obj)
        {
            var thermostats = new Dictionary<string, Thermostat>();
            foreach (var token in obj)
            {
                string key = ((JProperty)token).Name;
                thermostats.Add(key, new Thermostat(token.First));
            }
            _thermostats = thermostats;
        }

        private void ParseDevicesSmokeAlarms(JToken obj)
        {
            var smokeAlarms = new Dictionary<string, SmokeAlarm>();
            foreach (var token in obj)
            {
                string key = ((JProperty)token).Name;
                smokeAlarms.Add(key, new SmokeAlarm(token.First));
            }
            _smokeAlarms = smokeAlarms;
        }

        private void ParseDevicesCameras(JToken obj)
        {
            var cameras = new Dictionary<string, Camera>();
            foreach (var token in obj)
            {
                string key = ((JProperty)token).Name;
                cameras.Add(key, new Camera(token.First));
            }
            _cameras = cameras;
        }

        private void ParseStructures(JToken obj)
        {
            var structures = new Dictionary<string, Structure>();
            foreach (var token in obj)
            {
                string key = ((JProperty)token).Name;
                structures.Add(key, new Structure(token.First));
            }
            _structures = structures;
        }

        public DateTime Timestamp { get => _timestamp; }
        public string Path { get => _path; }
        public Metadata Metadata { get => _metadata; }        
        public Dictionary<string, Thermostat> Thermostats { get => _thermostats; }
        public Dictionary<string, SmokeAlarm> SmokeAlarms { get => _smokeAlarms; }
        public Dictionary<string, Camera> Cameras { get => _cameras; }
        public Dictionary<string, Structure> Structures { get => _structures; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{_timestamp.ToShortTimeString()} [StreamEvent: {_path}]");
            if (_metadata != null) { sb.AppendLine($"   {_metadata}"); }
            if (_thermostats != null) { foreach (var kvp in _thermostats) { sb.AppendLine($"   {kvp.Value}"); }}
            if (_smokeAlarms != null) { foreach (var kvp in _smokeAlarms) { sb.AppendLine($"   {kvp.Value}"); }}
            if (_cameras != null) { foreach (var kvp in _cameras) { sb.AppendLine($"   {kvp.Value}"); }}
            if (_structures != null) { foreach (var kvp in _structures) { sb.AppendLine($"   {kvp.Value}"); }}
            return sb.ToString();
        }
    }
}