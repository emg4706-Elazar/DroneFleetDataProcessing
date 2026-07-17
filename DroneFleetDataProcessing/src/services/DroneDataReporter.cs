using System;
using System.Collections.Generic;
using System.Text;
using DroneFleetDataProcessing.src.models;

namespace DroneFleetDataProcessing.src.services
{
    class DroneDataReporter
    {
        public string JenerateReport(FleetReport report)
        {
            StringBuilder reportBuilder = new StringBuilder();

            reportBuilder.AppendLine("DRONE FLEET ANALYSIS REPORT");
            reportBuilder.AppendLine();
            reportBuilder.AppendLine("PROCESSING SUMMARY");
            reportBuilder.AppendLine($"Total raw records: {report.TotalRecords}");
            reportBuilder.AppendLine($"Valid records: {report.ValidRecords}");
            reportBuilder.AppendLine($"Rejected records: {report.RejectedRecords}");
            reportBuilder.AppendLine();
            reportBuilder.AppendLine("NON-OPERATIONAL DRONES");

            if (report.NonOperationalDrones.Count == 0)
            {
                reportBuilder.AppendLine("No result found.");
            }
            else
            {
                foreach(string drone in report.NonOperationalDrones)
                {
                    reportBuilder.AppendLine(drone);
                }   
            }

            reportBuilder.AppendLine(". . .");
            reportBuilder.AppendLine();
            reportBuilder.AppendLine("TOP 5 DRONES BY FLIGHT HOURS");
            if (report.TopFiveDroneByFlightHours.Count == 0)
            {
                reportBuilder.AppendLine("No result found.");
            }
            else
            {
                reportBuilder.AppendLine($"1. {report.TopFiveDroneByFlightHours[0]}");
                reportBuilder.AppendLine($"2. {report.TopFiveDroneByFlightHours[1]}");
                reportBuilder.AppendLine($"3. {report.TopFiveDroneByFlightHours[2]}");
                reportBuilder.AppendLine($"4. {report.TopFiveDroneByFlightHours[3]}");
                reportBuilder.AppendLine($"5. {report.TopFiveDroneByFlightHours[4]}");
            }
            reportBuilder.AppendLine();
            reportBuilder.AppendLine("AVAILABLE DRONE MODELS");
    
            if (report.AvailableModels.Count == 0)
            {
                reportBuilder.AppendLine("No result found.");
            }
            else
            {
                foreach (string drone in report.AvailableModels)
                {
                    reportBuilder.AppendLine(drone);
                }
            }
            reportBuilder.AppendLine(". . .");
            reportBuilder.AppendLine();
            reportBuilder.AppendLine();

        }


    }
}
