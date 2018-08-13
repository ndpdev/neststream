using System;
using Newtonsoft.Json.Linq;

namespace NestStream.Type
{
    public class SmokeAlarm
    {
        public string DeviceID { get; }
        public string Locale { get; }
        public string SoftwareVersion { get; }
        public string StructureID { get; }
        public string Name { get; }
        public string NameLong { get; }
        public DateTime LastConnection { get; }
        public bool IsOnline { get; }
        public SmokeAlarmBatteryHealth BatteryHealth { get; }
        public SmokeAlarmState COAlarmState { get; }
        public SmokeAlarmState SmokeAlarmState { get; }
        public bool IsManualTestActive { get; }
        public DateTime LastManualTestTime { get; }
        public SmokeAlarmColorState UIColorState { get; }
        public string WhereID { get; }
        public string WhereName { get; }

        internal SmokeAlarm(JToken obj)
        {
            if (obj["device_id"] != null) { DeviceID = obj.Value<string>("device_id"); }
            if (obj["locale"] != null) { Locale = obj.Value<string>("locale"); }
            if (obj["software_version"] != null) { SoftwareVersion = obj.Value<string>("software_version"); }
            if (obj["structure_id"] != null) { StructureID = obj.Value<string>("strucure_id"); }
            if (obj["name"] != null) { Name = obj.Value<string>("name"); }
            if (obj["name_long"] != null) { NameLong = obj.Value<string>("name_long"); }
            if (obj["last_connection"] != null) { LastConnection = obj.Value<DateTime>("last_connection"); }
            if (obj["is_online"] != null) { IsOnline = obj.Value<bool>("is_online"); }
            if (obj["battery_health"] != null)
            {
                SmokeAlarmBatteryHealth batteryHealth;
                if (Enum.TryParse<SmokeAlarmBatteryHealth>(
                    obj.Value<string>("battery_health"),
                    out batteryHealth)) { BatteryHealth = batteryHealth; }
            }
            if (obj["co_alarm_state"] != null)
            {
                SmokeAlarmState coAlarmState;
                if (Enum.TryParse<SmokeAlarmState>(
                    obj.Value<string>("co_alarm_state"), 
                    out coAlarmState)) { COAlarmState = coAlarmState; }
            }
            if (obj["smoke_alarm_state"] != null)
            {
                SmokeAlarmState smokeAlarmState;
                if (Enum.TryParse<SmokeAlarmState>(
                    obj.Value<string>("smoke_alarm_state"),
                    out smokeAlarmState)) { SmokeAlarmState = smokeAlarmState; }
            }
            if (obj["is_manual_test_active"] != null) { IsManualTestActive = obj.Value<bool>("is_manual_test_Active"); }
            if (obj["last_manual_test_time"] != null) { LastManualTestTime = obj.Value<DateTime>("last_manual_test_time"); }
            if (obj["ui_color_state"] != null)
            {
                SmokeAlarmColorState uiColorState;
                if (Enum.TryParse<SmokeAlarmColorState>(
                    obj.Value<string>("ui_color_state"), 
                    out uiColorState)) { UIColorState = uiColorState; }
            }
            if (obj["where_id"] != null) { WhereID = obj.Value<string>("where_id"); }
            if (obj["where_name"] != null) { WhereName = obj.Value<string>("where_name"); }
        }

        public override string ToString()
        {
            return string.Concat($"[SmokeAlarm {Name}] ID:{DeviceID} Online:{IsOnline} UIColor:{UIColorState} ",
                $"Battery:{BatteryHealth} COAlarm:{COAlarmState} SmokeAlarm:{SmokeAlarmState}");
        }
    }

    public enum SmokeAlarmBatteryHealth
    {
        ok,
        replace
    }

    public enum SmokeAlarmState
    {
        ok,
        warning,
        emergency
    }

    public enum SmokeAlarmColorState
    {
        gray,
        green,
        yellow,
        red
    }
}