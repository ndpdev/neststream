using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace NestStream
{
    public class ThermostatData
    {
        private string _deviceId;
        private string _name;
        private bool _isOnline;
        private short _temperature;
        private short _humidity;
        private char _hvacMode;
        private char _hvacState;
        private bool _isFanActive;

        public static IEnumerable<ThermostatData> Parse(string jsonInput)
        {
            var json = JObject.Parse(jsonInput);
            if (json["data"] == null || json["data"]["thermostats"] == null) { yield break; }

            foreach (var thermostat in json["data"]["thermostats"].Values())
            {
                ThermostatData data = new ThermostatData();
                data._deviceId = thermostat.Value<string>("device_id");
                data._name = thermostat.Value<string>("name");
                data._isOnline = thermostat.Value<bool>("is_online");
                data._temperature = thermostat.Value<short>("ambient_temperature_f");
                data._humidity = thermostat.Value<short>("humidity");

                switch (thermostat.Value<string>("hvac_mode"))
                {
                    case "heat": { data._hvacMode = 'H'; break; }
                    case "cool": { data._hvacMode = 'C'; break; }
                    case "heat-cool": { data._hvacMode = 'X'; break; }
                    case "eco": { data._hvacMode = 'E'; break; }
                    case "off": { data._hvacMode = 'O'; break; }
                    default: { data._hvacMode = '?'; break; }
                }

                switch (thermostat.Value<string>("hvac_state"))
                {
                    case "heating": { data._hvacState = 'H'; break; }
                    case "cooling": { data._hvacState = 'C'; break; }
                    case "off": { data._hvacState = 'O'; break; }
                    default: { data._hvacState = '?'; break; }
                }

                data._isFanActive = thermostat.Value<bool>("fan_timer_active");
                yield return data;
            }
        }

        public string DeviceID { get => _deviceId; }
        public string Name { get => _name; }
        public bool IsOnline { get => _isOnline; }
        public short Temperature { get => _temperature; }
        public short Humidity { get => _humidity; }
        public char HvacMode { get => _hvacMode; }
        public char HvacState { get => _hvacState; }
        public bool IsFanActive { get => _isFanActive; }

        public override string ToString()
        {
            return string.Concat($"[{_deviceId} - {_name}] Online:{_isOnline} ",
                $"Temperature:{_temperature} Humidity:{_humidity} HvacMode:{_hvacMode} ",
                $"HvacState:{_hvacState} FanActive:{_isFanActive}");
        }
    }
}