using DroneFleetDataProcessing.src.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DroneFleetDataProcessing.src.models;
using DroneFleetDataProcessing.src.exceptions;
using DroneFleetDataProcessing.src.validators;

namespace DroneFleetDataProcessing.src
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("=== Drone Fleet Data Processing System ===\n");

            string filepathInput = GetInputPath("drones_raw.json");
            string filenameOutput = "drones_clean.json";

            string currentStepText = "Step 1: Reading raw data...";
            try
            {
                DroneDataLoader loader = new DroneDataLoader();
                List<Drone> allDrones = loader.Load(filepathInput);
                Console.WriteLine($"{currentStepText} Read {allDrones.Count} records from raw file");

                currentStepText = "Step 2: Validating data and creating clean dataset...";
                List<Drone> validDrones = new List<Drone>();
                List<Drone> rejectedDrones = new List<Drone>();

                var validator = new DroneValidator();
                var storer = new DronesDataValidator(validator);

                storer.ValidateFleet(allDrones, validDrones, rejectedDrones);
                Console.WriteLine($"{currentStepText} Valid records: {validDrones.Count} Rejected records: {rejectedDrones.Count}");

                currentStepText = "Step 3: Saving clean data...";
                DroneDataSaver saver = new DroneDataSaver();
                saver.Save(validDrones, filenameOutput);

                string fullOutputPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "output", filenameOutput));
                Console.WriteLine($"{currentStepText} Clean data saved to: {fullOutputPath}");

                currentStepText = "Step 4: Reloading clean data...";
                List<Drone> reloadedDrones = loader.Load(fullOutputPath);
                Console.WriteLine($"{currentStepText} Loaded {reloadedDrones.Count} records from clean dataset");

                currentStepText = "Step 5: Performing analysis...";
                DroneAnalyzer analyzer = new DroneAnalyzer();
                FleetReport report = analyzer.GenerateReport(reloadedDrones, allDrones.Count, rejectedDrones.Count);
                Console.WriteLine($"{currentStepText} Analysis completed successfully");

                currentStepText = "Step 6: Generating report...";

                DroneDataReporter reporter = new DroneDataReporter();
                reporter.GenerateAndSaveReport(report, "analysis_report.txt");

                string fullReportPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "output", "analysis_report.txt"));
                Console.WriteLine($"{currentStepText} Report generated successfully: {fullReportPath}");

                Console.WriteLine("\n=== Process completed successfully! ===");
            }
            catch (Exception e)
            {
                Console.WriteLine(currentStepText);
                Console.WriteLine($"Error: {e.GetType().Name} - {e.Message}");
            }
        }

        private static string GetInputPath(string filename)
        {
            string filepath = Path.Combine(
                AppContext.BaseDirectory,
                "input",
                "raw",
                filename);

            return filepath;
        }
    }
}