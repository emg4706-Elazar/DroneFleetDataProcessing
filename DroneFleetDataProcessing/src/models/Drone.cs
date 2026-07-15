using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DroneFleetDataProcessing.src.models
{
    class Drone
    {
        public int id { get; set; }
        public string serialNumber { get; set; }
        public string model { get; set; }
        public string category { get; set; }

        [JsonPropertyName("base_location")]
        public string baseLocation { get; set; }
        public double flightHours { get; set; }
        public int batteryHealth { get; set; }
        public double maxRangeKm { get; set; }
        public int missionsCompleted { get; set; }
        public string status { get; set; }

        public string ToSummary() =>
            $"ID: {id}," +
            $" Serial Number: {serialNumber}," +
            $" Model: {model}," +
            $" Category: {category}," +
            $" Status: {status}," +
            $" Base: {baseLocation}," +
            $" Battery: {batteryHealth}%," +
            $" Flight Hours: {flightHours}";
    }
}
