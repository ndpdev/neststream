using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace NestStream.Type
{
    public class Structure
    {
        public string StructureID { get; }
        public List<string> Thermostats { get; }
        public List<string> SmokeAlarms { get; }
        public List<string> Cameras { get; }
        public StructureState Away { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; }
        public string PostalCode { get; }
        public DateTime? PeakPeriodStartTime { get; }
        public DateTime? PeakPeriodEndTime { get; }
        public string TimeZone { get; }
        public DateTime? EtaBegin { get; }
        public StructureAlarmState? COAlarmState { get; }
        public StructureAlarmState? SmokeAlarmState { get; }
        public bool RhrEnrollment { get; }
        public StructureSecurityState? WwnSecurityState { get; }
        public Dictionary<string, StructureWhere> Wheres { get; }

        internal Structure(JToken obj)
        {
            if (obj["structure_id"] != null) { StructureID = obj.Value<string>("structure_id"); }
            if (obj["thermostats"] != null)
            {
                var thermostats = new List<string>();
                foreach (JToken item in obj["thermostats"]) { thermostats.Add(item.Value<string>()); }
                Thermostats = thermostats;
            }
            if (obj["smoke_co_alarms"] != null)
            {
                var smokeAlarms = new List<string>();
                foreach (JToken item in obj["smoke_co_alarms"]) { smokeAlarms.Add(item.Value<string>()); }
                SmokeAlarms = smokeAlarms;
            }
            if (obj["cameras"] != null)
            {
                var cameras = new List<string>();
                foreach (JToken item in obj["cameras"]) { cameras.Add(item.Value<string>()); }
                Cameras = cameras;
            }
            if (obj["away"] != null)
            {
                StructureState awayState;
                if (Enum.TryParse<StructureState>(
                    obj.Value<string>("away"),
                    out awayState)) { Away = awayState; }
            }
            if (obj["name"] != null) { Name = obj.Value<string>("name"); }
            if (obj["country_code"] != null) { CountryCode = obj.Value<string>("country_code"); }
            if (obj["postal_code"] != null) { PostalCode = obj.Value<string>("postal_code"); }
            if (obj["peak_period_start_time"] != null) { PeakPeriodStartTime = obj.Value<DateTime>("peak_period_start_time"); }
            if (obj["peak_period_end_time"] != null) { PeakPeriodEndTime = obj.Value<DateTime>("peak_period_end_time"); }
            if (obj["eta_begin"] != null) { EtaBegin = obj.Value<DateTime>("eta_begin"); }
            if (obj["co_alarm_state"] != null)
            {
                StructureAlarmState coAlarmState;
                if (Enum.TryParse<StructureAlarmState>(
                    obj.Value<string>("co_alarm_state"), 
                    out coAlarmState)) { COAlarmState = coAlarmState; }
            }
            if (obj["smoke_alarm_state"] != null)
            {
                StructureAlarmState smokeAlarmState;
                if (Enum.TryParse<StructureAlarmState>(
                    obj.Value<string>("smoke_alarm_state"), 
                    out smokeAlarmState)) { SmokeAlarmState = smokeAlarmState; }
            }
            if (obj["rhr_enrollment"] != null) { RhrEnrollment = obj.Value<bool>("rhr_enrollment"); }
            if (obj["wwn_security_state"] != null)
            {
                StructureSecurityState securityState;
                if (Enum.TryParse<StructureSecurityState>(
                    obj.Value<string>("wwn_security_state"), 
                    out securityState)) { WwnSecurityState = securityState; }
            }
            if (obj["wheres"] != null)
            {
                var wheres = new Dictionary<string, StructureWhere>();
                foreach (JToken token in obj["wheres"])
                {
                    var key = ((JProperty)token).Name;
                    var whereObj = token.First;
                    wheres.Add(key, new StructureWhere(whereObj));
                }
                Wheres = wheres;
            }
        }

        public override string ToString()
        {
            return $"[Structure {Name}] ID:{StructureID}";
        }
    }

    public enum StructureAlarmState
    {
        ok,
        warning,
        emergency
    }

    public enum StructureState
    {
        home,   // Home state
        away,   // Away state
        unknown // Structure has no devices
    }

    public enum StructureSecurityState
    {
        ok,     // OK
        deter   // Person detected in perimeter camera
                // while structure is on away state
    }
}