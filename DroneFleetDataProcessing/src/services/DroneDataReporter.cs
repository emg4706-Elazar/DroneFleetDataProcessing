using System;
using System.Collections.Generic;
using System.Text;
using DroneFleetDataProcessing.src.models;
using DroneFleetDataProcessing.src.exceptions;
using System.Reflection.Metadata;



namespace DroneFleetDataProcessing.src.services
{
    class DroneDataReporter
    {

        public void GenerateAndSaveReport(FleetReport report, string filename)
        {
            string contentReport = GenerateReport(report);

            try
            {
                FileWriter writer = new FileWriter();
                writer.Write(filename, contentReport);
            }
            catch (ArgumentNullException e)
            {
                throw new DroneDataReporterException(e.Message);
            }
            catch (InvalidPathException e)
            {
                throw new DroneDataReporterException(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                throw new DroneDataReporterException(e.Message);
            }
            catch (IOException e)
            {
                throw new DroneDataReporterException(e.Message);
            }
        }


        public string GenerateReport(FleetReport report)
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
                reportBuilder.AppendLine("No results found.");
            }
            else
            {
                foreach (string drone in report.NonOperationalDrones)
                {
                    reportBuilder.AppendLine(drone);
                }   
            }

            reportBuilder.AppendLine("...");
            reportBuilder.AppendLine();

            reportBuilder.AppendLine("TOP 5 DRONES BY FLIGHT HOURS");

            if (report.TopFiveDroneByFlightHours.Count == 0)
            {
                reportBuilder.AppendLine("No results found.");
            }
            else
            {
                for (int i = 0; i < report.TopFiveDroneByFlightHours.Count; i++)
                {
                    reportBuilder.AppendLine(
                        $"{i + 1}. {report.TopFiveDroneByFlightHours[i]}");
                }
            }
            reportBuilder.AppendLine();

            reportBuilder.AppendLine("AVAILABLE DRONE MODELS");   
            if (report.AvailableModels.Count == 0)
            {
                reportBuilder.AppendLine("No results found.");
            }
            else
            {
                foreach (string model in report.AvailableModels)
                {
                    reportBuilder.AppendLine(model);
                }
            }
            reportBuilder.AppendLine("...");
            reportBuilder.AppendLine();

            reportBuilder.AppendLine("DRONES BY BASE");

            if (report.DronesByBase.Count == 0)
            {
                reportBuilder.AppendLine("No results found.");
            }
            else
            {
                foreach (KeyValuePair<string, int> baseEntry in report.DronesByBase)
                {
                    reportBuilder.AppendLine($"{baseEntry.Key}: {baseEntry.Value}");
                }
            }
            reportBuilder.AppendLine();

            reportBuilder.AppendLine("AVERAGE BATTERY HEALTH BY MODEL");

            if (report.AverageBatteryByModel.Count == 0)
            {
                reportBuilder.AppendLine("No results found.");
            }
            else
            {
                foreach (KeyValuePair<string, double> modelEntry in report.AverageBatteryByModel)
                {
                    reportBuilder.AppendLine($"{modelEntry.Key}: {modelEntry.Value:F2}");
                }
            }
            reportBuilder.AppendLine();

            reportBuilder.AppendLine("MODEL WITH HIGHEST TOTAL COMPLETED MISSIONS");

            reportBuilder.AppendLine($"Model: {report.BestModelByMissions}");
            reportBuilder.AppendLine($"Total completed missions: {report.BestModelTotalMissions}");
            reportBuilder.AppendLine();

            reportBuilder.AppendLine("SELECTED ADDITIONAL ANALYSIS");
            reportBuilder.AppendLine($"Analysis name: Operational drone bases analysis");
            if (report.AdditionalAnalysisLines.Count == 0)
            {
                reportBuilder.AppendLine("No results found.");
            }
            else
            {
                foreach (string baseLocation in report.AdditionalAnalysisLines)
                {
                    reportBuilder.AppendLine(baseLocation);
                }
            }
 

            return reportBuilder.ToString();
        }


    }
}
