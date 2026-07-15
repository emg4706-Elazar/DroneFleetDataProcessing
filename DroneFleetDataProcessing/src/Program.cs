using DroneFleetDataProcessing.src.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DroneFleetDataProcessing.src.models;
using DroneFleetDataProcessing.src.exceptions;



namespace DroneFleetDataProcessing.src
{
    class Program
    {
        public static void Main()
        {
            string filepathInput = GetInputPath("drones_raw.json");
            string filepathOutput = GetOutputPath("drones_clean.json");
            
            try
            {
                DroneDataLoader loader = new DroneDataLoader();
                List<Drone> drones = loader.Load(filepathInput);


                DroneDataSaver saver = new DroneDataSaver();
                saver.Save(drones, filepathOutput);
                Console.WriteLine("The Process completed successfuly");

            }
            catch (DroneDataLoaderException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            catch(DroneDataSaverException e)
            {
                Console.WriteLine($"Error: {e.Message}");
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

        private static string GetOutputPath(string filename)
        {
            string outputDirectory = Path.Combine(
                AppContext.BaseDirectory,
                "..",
                "..",
                "..",
                "output");
            
            Directory.CreateDirectory(outputDirectory);

            string outputPath = Path.Combine(
            outputDirectory,
            filename);
  
            return outputPath;
        }    
    }
}
