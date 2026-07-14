using DroneFleetDataProcessing.src.models;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;

namespace DroneFleetDataProcessing.src.validators
{
    class DroneValidator : IDroneValidator
    {
        private const int MinBatteryHealth = 0;
        private const int MaxBatteryHealth = 100;
        private const double MinFlightHours = 0.0;
        private const double MaxFlightHours = 2500.0;
        private const double MinMaxRangeKm = 1.0;
        private const double MaxMaxRangeKm = 150.0;
        private const int MinMissionsCompleted = 0;
        private const int MaxMissionsCompleted = 5000;
        private const int MinOperationalBattery = 20;

        private static readonly Regex SerialNumberPattern = new Regex(@"^DR-\d{4}$");

        private static readonly string[] ValidModels = { "Falcon-X", "Raven-M", "SkyEye-2", "CargoBee", "Storm-4", "Scout-Lite" };
        public enum ValidCategory  { Recon, Patrol, Mapping, Delivery, Search };
        public enum ValidBaseLocation  { North, South, Central, East, West };
        public enum ValidStatus { Operational, Maintenance, Grounded, Training };

        public bool IsValid(Drone drone)
        {
            if (drone == null) { return false; }

            if (!IsValidId(drone.id)) { return false; }
            if (!IsValidFlightHours(drone.flightHours)) { return false; }
            if (!IsValidBatteryHealth(drone.batteryHealth)) { return false; }
            if (!IsValidMaxRangeKm(drone.maxRangeKm)) { return false; }
            if (!IsValidMissionsCompleted(drone.missionsCompleted)) { return false; }
            if (!IsValidSerialNumber(drone.serialNumber)) { return false; }

            if (!IsValidStatus(drone.status)) { return false; }
            if (!IsValidCategory(drone.category)) { return false; }
            if (!IsValidModel(drone.model)) { return false; }
            if (!IsValidBaseLocation(drone.baseLocation)) { return false; }

            if (!MeetsOperationalStandard(drone.batteryHealth, drone.status)) { return false; }

            return true;
        }

        private bool IsValidId(int id)
        {
            return id > 0;
        }
        private bool IsValidFlightHours(double hours)
        {
            return hours >= MinFlightHours && hours <= MaxFlightHours;
        }
        private bool IsValidBatteryHealth(int batteryHealth)
        {
            return batteryHealth >= MinBatteryHealth && batteryHealth <= MaxBatteryHealth;
        }
        private bool IsValidMaxRangeKm(double maxRangeKm)
        {
            return maxRangeKm >= MinMaxRangeKm && maxRangeKm <= MaxMaxRangeKm;
        }
        private bool IsValidMissionsCompleted(int missionsCompleted)
        {
            return missionsCompleted >= MinMissionsCompleted && missionsCompleted <= MaxMissionsCompleted;
        }
        private bool IsValidStatus(string status)
        {
            return Enum.IsDefined(typeof(ValidStatus), status);
        }
        private bool IsValidCategory(string category)
        {
            return Enum.IsDefined(typeof(ValidCategory), category);
        }
        private bool IsValidBaseLocation(string baseLocation)
        {
            return Enum.IsDefined(typeof(ValidBaseLocation), baseLocation);
        }
        private bool IsValidModel(string model)
        {
            return ValidModels.Contains(model);
        }
        private bool MeetsOperationalStandard(int battery, string status)
        {
            if (battery < MinOperationalBattery && status == "Operational"){ return false; }

            return true;
        }
        private bool IsValidSerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber)) { return false; }
            return SerialNumberPattern.IsMatch(serialNumber);
        }

    }
}
