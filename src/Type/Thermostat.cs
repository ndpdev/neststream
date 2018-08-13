using System;
using Newtonsoft.Json.Linq;

namespace NestStream.Type
{
    public class Thermostat
    {
        public string DeviceID { get; }
        public string Locale { get; }
        public string SoftwareVersion { get; }
        public string StructureID { get; }
        public string Name { get; }
        public string NameLong { get; }
        public DateTime LastConnection { get; }
        public bool IsOnline { get; }
        public bool CanCool { get; }
        public bool CanHeat { get; }
        public bool IsUsingEmergencyHeat { get; }
        public bool HasFan { get; }
        public bool FanTimerActive { get; set; }
        public DateTime FanTimerTimeout { get; }
        public bool HasLeaf { get; }
        public ThermostatTemperatureScale TemperatureScale { get; set; }
        public int TargetTemperatureF { get; set; }
        public double TargetTemperatureC { get; set; }
        public int TargetTemperatureHighF { get; set; }
        public double TargetTemperatureHighC { get; set; }
        public int TargetTemperatureLowF { get; set; }
        public double TargetTemperatureLowC { get; set; }
        public int EcoTemperatureHighF { get; }
        public double EcoTemperatureHighC { get; }
        public int EcoTemperatureLowF { get; }
        public double EcoTemperatureLowC { get; }
        public int AwayTemperatureHighF { get; }
        public double AwayTemperatureHighC { get; }
        public int AwayTemperatureLowF { get; }
        public double AwayTemperatureLowC { get; }
        public ThermostatHvacMode HvacMode { get; set; }
        public ThermostatHvacMode PreviousHvacMode { get; }
        public int AmbientTemperatureF { get; }
        public double AmbientTemperatureC { get; }
        public int Humidity { get; }
        public ThermostatHvacState HvacState {get; }
        public string WhereID { get; }
        public bool IsLocked { get; }
        public int LockedTempMinF { get; }
        public int LockedTempMaxF { get; }
        public double LockedTempMinC { get; }
        public double LockedTempMaxC { get; }
        public string Label { get; set; }
        public string WhereName { get; }
        public bool SunlightCorrectionEnabled { get; }
        public bool SunlightCorrectionActive { get; }
        public int FanTimerDuration { get; set; }
        public string TimeToTarget { get; }
        public ThermostatTrainingState TimeToTargetTraining { get; }

        internal Thermostat(JToken obj)
        {
            if (obj["device_id"] != null) { DeviceID = obj.Value<string>("device_id"); }
            if (obj["locale"] != null) { Locale = obj.Value<string>("locale"); }
            if (obj["software_version"] != null) { SoftwareVersion = obj.Value<string>("software_version"); }
            if (obj["structure_id"] != null) { StructureID = obj.Value<string>("structure_id"); }
            if (obj["name"] != null) { Name = obj.Value<string>("name"); }
            if (obj["name_long"] != null) { NameLong = obj.Value<string>("name_long"); }
            if (obj["last_connection"] != null) { LastConnection = obj.Value<DateTime>("last_connection"); }
            if (obj["is_online"] != null) { IsOnline = obj.Value<bool>("is_online"); }
            if (obj["can_cool"] != null) { CanCool = obj.Value<bool>("can_cool"); }
            if (obj["can_heat"] != null) { CanHeat = obj.Value<bool>("can_heat"); }
            if (obj["is_using_emergency_heat"] != null) { IsUsingEmergencyHeat = obj.Value<bool>("is_using_emergency_heat"); }
            if (obj["has_fan"] != null) { HasFan = obj.Value<bool>("has_fan"); }
            if (obj["fan_timer_active"] != null) { FanTimerActive = obj.Value<bool>("fan_timer_active"); }
            if (obj["fan_timer_timeout"] != null) { FanTimerTimeout = obj.Value<DateTime>("fan_timer_timeout"); }
            if (obj["has_leaf"] != null) { HasLeaf = obj.Value<bool>("has_leaf"); }
            if (obj["temperature_scale"] != null)
            {
                ThermostatTemperatureScale temperatureScale;
                if (Enum.TryParse<ThermostatTemperatureScale>(
                    obj.Value<string>("temperature_scale"),
                    out temperatureScale)) { TemperatureScale = temperatureScale; }
            }
            if (obj["target_temperature_f"] != null) { TargetTemperatureF = obj.Value<int>("target_temperature_f"); }
            if (obj["target_temperature_c"] != null) { TargetTemperatureC = obj.Value<double>("target_temperature_c"); }
            if (obj["target_temperature_high_f"] != null) { TargetTemperatureHighF = obj.Value<int>("target_temperature_high_f"); }
            if (obj["target_temperature_high_c"] != null) { TargetTemperatureHighC = obj.Value<double>("target_temperature_high_c"); }
            if (obj["target_temperature_low_f"] != null) { TargetTemperatureLowF = obj.Value<int>("target_temperature_low_f"); }
            if (obj["target_temperature_low_c"] != null) { TargetTemperatureLowC = obj.Value<double>("target_temperature_low_c"); }
            if (obj["eco_temperature_high_f"] != null) { EcoTemperatureHighF = obj.Value<int>("eco_temperature_high_f"); }
            if (obj["eco_temperature_high_c"] != null) { EcoTemperatureHighC = obj.Value<double>("eco_temperature_high_c"); }
            if (obj["eco_temperature_low_f"] != null) { EcoTemperatureLowF = obj.Value<int>("eco_temperature_low_f"); }
            if (obj["eco_temperature_low_c"] != null) { EcoTemperatureLowC = obj.Value<double>("eco_temperature_low_c"); }
            if (obj["away_temperature_high_f"] != null) { AwayTemperatureHighF = obj.Value<int>("away_temperature_high_f"); }
            if (obj["away_temperature_high_c"] != null) { AwayTemperatureHighC = obj.Value<double>("away_temperature_high_c"); }
            if (obj["away_temperature_low_f"] != null) { AwayTemperatureLowF = obj.Value<int>("away_temperature_low_f"); }
            if (obj["away_temperature_low_c"] != null) { AwayTemperatureLowC = obj.Value<double>("away_temperature_low_c"); }
            if (obj["hvac_mode"] != null)
            {
                ThermostatHvacMode hvacMode;
                if (Enum.TryParse<ThermostatHvacMode>(
                    obj.Value<string>("hvac_mode").Replace("-", String.Empty),
                    out hvacMode)) { HvacMode = hvacMode; }
            }
            if (obj["previous_hvac_mode"] != null)
            {
                ThermostatHvacMode previousHvacMode;
                if (Enum.TryParse<ThermostatHvacMode>(
                    obj.Value<string>("previous_hvac_mode").Replace("-", String.Empty),
                    out previousHvacMode)) { PreviousHvacMode = previousHvacMode; }
            }
            if (obj["ambient_temperature_f"] != null) { AmbientTemperatureF = obj.Value<int>("ambient_temperature_f"); }
            if (obj["ambient_temperature_c"] != null) { AmbientTemperatureC = obj.Value<double>("ambient_temperature_c"); }
            if (obj["humidity"] != null) { Humidity = obj.Value<int>("humidity"); }
            if (obj["hvac_state"] != null)
            {
                ThermostatHvacState hvacState;
                if (Enum.TryParse<ThermostatHvacState>(
                    obj.Value<string>("thermostat_hvac_state"),
                    out hvacState)) { HvacState = hvacState; }
            }
            if (obj["where_id"] != null) { WhereID = obj.Value<string>("where_id"); }
            if (obj["is_locked"] != null) { IsLocked = obj.Value<bool>("is_locked"); }
            if (obj["locked_temp_min_f"] != null) { LockedTempMinF = obj.Value<int>("locked_temp_min_f"); }
            if (obj["locked_temp_max_f"] != null) { LockedTempMaxF = obj.Value<int>("locked_temp_max_f"); }
            if (obj["locked_temp_min_c"] != null) { LockedTempMinC = obj.Value<double>("locked_temp_min_c"); }
            if (obj["locked_temp_max_c"] != null) { LockedTempMaxC = obj.Value<double>("locked_temp_max_c"); }
            if (obj["label"] != null) { Label = obj.Value<string>("label"); }
            if (obj["where_name"] != null) { WhereName = obj.Value<string>("where_name"); }
            if (obj["sunlight_correction_enabled"] != null) { SunlightCorrectionEnabled = obj.Value<bool>("sunlight_correction_enabled"); }
            if (obj["sunlight_correction_active"] != null) { SunlightCorrectionActive = obj.Value<bool>("sunlight_correction_active"); }
            if (obj["fan_timer_duration"] != null) { FanTimerDuration = obj.Value<int>("fan_timer_duration"); }
            if (obj["time_to_target"] != null) { TimeToTarget = obj.Value<string>("time_to_target"); }
            if (obj["time_to_target_training"] != null)
            {
                ThermostatTrainingState trainingState;
                if (Enum.TryParse<ThermostatTrainingState>(
                    obj.Value<string>("time_to_target_training"),
                    out trainingState)) { TimeToTargetTraining = trainingState; }
            }
        }

        public override string ToString()
        {
            return string.Concat($"[Thermostat {Name}] ID:{DeviceID} Online:{IsOnline} ",
                $"Temperature:{AmbientTemperatureF} Humidity:{Humidity} HvacMode:{HvacMode} ",
                $"HvacState:{HvacState} FanActive:{FanTimerActive}");
        }
    }

    public enum ThermostatTemperatureScale
    {
        C,
        F
    }

    public enum ThermostatHvacMode
    {
        heat,
        cool,
        heatcool,
        eco,
        off,
        blank
    }

    public enum ThermostatHvacState
    {
        heating,
        cooling,
        off
    }

    public enum ThermostatTrainingState
    {
        training,
        ready
    }

}