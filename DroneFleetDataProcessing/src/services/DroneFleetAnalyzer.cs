using DroneFleetDataProcessing.src.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneFleetDataProcessing.src.services
{
    class DroneAnalyzer
    {
        public FleetReport GenerateReport(List<Drone> validDrones, int allDrones, int rejectedDrones)
        {
            return new FleetReport
            {
                ValidRecords = validDrones.Count,
                TotalRecords = allDrones,
                RejectedRecords = rejectedDrones,
                NonOperationalDrones = GetNonOperationalDrones(validDrones),
                TopFiveDroneByFlightHours = GetTopFiveDroneByFlightHours(validDrones),
                AvailableModels = GetAvailableModels(validDrones),
                DroneByBase = GetDroneByBase(validDrones),
                AverageBatteryByModel = GetAverageBatteryByModel(validDrones),
                BestModelByMissions = GetBestModelByMissions(validDrones),
                BestModelTotalMissions = BestModelTotalMissions(validDrones)

            };

        }
        private List<string> GetNonOperationalDrones(List<Drone> drones)
        {
            if (drones == null)
            {
                return [];
            }

            return drones.Where(d => d.status != nameof(DroneStatus.Operational))
                .Select(d => $"{d.serialNumber} | {d.model} | {d.baseLocation} | {d.status}")
                .ToList();
        }

        private List<string> GetTopFiveDroneByFlightHours(List<Drone> drones)
        {
            if (drones == null)
            {
                return [];
            }

            return drones.OrderByDescending(d => d.flightHours)
                .Select(d => $"{d.serialNumber} | {d.model} | {d.flightHours}")
                .Take(5)
                .ToList();
        }

        private List<string> GetAvailableModels(List<Drone> drones)
        {
            if (drones == null)
            {
                return [];
            }

            return drones.Select(d => d.model).Distinct().ToList();
        }

        private Dictionary<string, int> GetDroneByBase(List<Drone> drones)
        {
            if (drones == null)
            {
                return [];
            }

            return drones.GroupBy(d => d.baseLocation)
                .ToDictionary(d => d.Key, d => d.Count());
        }

        private Dictionary<string, double> GetAverageBatteryByModel(List<Drone> drones)
        {
            if (drones == null)
            {
                return [];
            }

            return drones.GroupBy(d => d.model).
                ToDictionary(d => d.Key, d => d.Average(d => d.batteryHealth));
        }

        private string GetBestModelByMissions(List<Drone> drones)
        {
            if (drones == null || !drones.Any()) return "N/A";

            return drones.GroupBy(d => d.model)
                    .OrderByDescending(g => g.Sum(d => d.missionsCompleted))
                    .Select(g => g.Key) 
                    .FirstOrDefault() ?? "N/A";
        }

        private int BestModelTotalMissions(List<Drone> drones)
        {
            return drones.GroupBy(d => d.model)
                    .OrderByDescending(g => g.Sum(d => d.missionsCompleted))
                    .Select(g => g.Sum(d => d.missionsCompleted))
                    .First();
        }
    }
}
