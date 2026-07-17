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
            string filepathInput = "drones_raw.json";
            string filenameOutput = "drones_clean.json";

            try
            {
                DroneDataLoader loader = new DroneDataLoader();
                List<Drone> allDrones = loader.Load(filepathInput);

                List<Drone> validDrones = new List<Drone>();
                List<Drone> rejectedDrones = new List<Drone>();

                var validator = new DroneValidator();
                var storer = new DronesDataValidator(validator);

                storer.ValidateFleet(allDrones, validDrones, rejectedDrones);
 
                DroneDataSaver saver = new DroneDataSaver();
                saver.Save(validDrones, filenameOutput);

                DroneAnalyzer analyzer = new DroneAnalyzer();
                FleetReport report = analyzer.AnalyzeReport(validDrones, allDrones.Count, rejectedDrones.Count);

                DroneDataReporter reporter = new DroneDataReporter();
                reporter.GenerateAndSaveReport(report, "analysis_report.txt");
            }

            catch (DroneDataLoaderException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            catch (DroneDataSaverException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            catch (DroneDataReporterException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

    }
}
